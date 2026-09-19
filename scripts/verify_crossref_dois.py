import os
import sys
import json
import re
import urllib.request
import urllib.error

# Ensure UTF-8 output on Windows
if sys.platform == "win32":
    import io
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding="utf-8")

def query_crossref(doi):
    clean_doi = doi.replace("https://doi.org/", "").replace("http://doi.org/", "").strip()
    url = f"https://api.crossref.org/works/{clean_doi}"
    req = urllib.request.Request(url, headers={"User-Agent": "IVRAR_PBL_Auditor/1.0 (mailto:engineering.audit@education.org)"})
    try:
        with urllib.request.urlopen(req, timeout=12) as response:
            if response.status == 200:
                data = json.loads(response.read().decode("utf-8"))
                msg = data.get("message", {})
                title = msg.get("title", [""])[0]
                
                # Extract year
                year = None
                for date_field in ["published-print", "published-online", "issued", "created"]:
                    if date_field in msg and "date-parts" in msg[date_field]:
                        parts = msg[date_field]["date-parts"]
                        if parts and parts[0] and parts[0][0]:
                            year = parts[0][0]
                            break
                return True, year, title, clean_doi
    except Exception as e:
        return False, None, str(e), clean_doi
    return False, None, "Unknown error", clean_doi

def verify_group_papers(group_folder):
    roster_path = os.path.join(group_folder, "docs", "TEAM_ROSTER.json")
    if not os.path.exists(roster_path):
        return False, f"Missing TEAM_ROSTER.json in {group_folder}"
    
    with open(roster_path, "r", encoding="utf-8") as f:
        data = json.load(f)
        
    papers = data.get("foundational_papers", [])
    if len(papers) != 6:
        return False, f"Expected exactly 6 papers, found {len(papers)}"
        
    seminal_count = 0
    recent_count = 0
    errors = []
    
    print(f"\n--- Auditing Papers for {os.path.basename(group_folder)} ---")
    for p in papers:
        pid = p.get("id")
        pdoi = p.get("doi", "")
        pyear = p.get("year")
        ptitle = p.get("title", "")
        
        ok, cr_year, cr_title, clean_doi = query_crossref(pdoi)
        if not ok:
            errors.append(f"Paper {pid} DOI failed CrossRef lookup: {pdoi} ({cr_title})")
            continue
            
        print(f"[{pid}] CrossRef: {cr_year} | {clean_doi} | {cr_title[:60]}...")
        
        # Check year category
        # Seminal: prior to 2022
        # Recent: 2022 - 2026
        if cr_year and cr_year >= 2022:
            recent_count += 1
        else:
            seminal_count += 1
            
    print(f"Results: {seminal_count} Seminal, {recent_count} Recent (2022-2026)")
    if seminal_count != 2 or recent_count != 4:
        errors.append(f"Strict 2:4 ratio violated! Found {seminal_count} seminal and {recent_count} recent papers. Expected 2 seminal : 4 recent (2022-2026).")
        
    if errors:
        return False, "\n".join(errors)
    return True, f"Verified 2 seminal + 4 recent papers with 100% active DOIs."

if __name__ == "__main__":
    if len(sys.argv) > 1:
        target = sys.argv[1]
        ok, msg = verify_group_papers(target)
        print(msg)
        sys.exit(0 if ok else 1)
    else:
        print("Usage: python verify_crossref_dois.py <path_to_group_folder>")

import sys
import json
import urllib.request
import unicodedata

def clean(text):
    if not text:
        return ""
    return unicodedata.normalize('NFKD', str(text)).encode('ascii', 'ignore').decode('ascii')

dois = sys.argv[1:]
for d in dois:
    url = f"https://api.crossref.org/works/{d}"
    req = urllib.request.Request(url, headers={"User-Agent": "NMIMS-Audit/1.0 (mailto:audit@nmims.edu)"})
    try:
        with urllib.request.urlopen(req) as resp:
            data = json.loads(resp.read().decode("utf-8"))["message"]
            title = data.get("title", [""])[0]
            year = data.get("issued", {}).get("date-parts", [[None]])[0][0]
            authors = ", ".join([f"{a.get('given', '')} {a.get('family', '')}".strip() for a in data.get("author", [])])
            container = data.get("container-title", [""])[0]
            print(f"DOI: {d}")
            print(f"Year: {year}")
            print(f"Title: {clean(title)}")
            print(f"Authors: {clean(authors)}")
            print(f"Container: {clean(container)}")
            print("-" * 50)
    except Exception as e:
        print(f"Error {d}: {e}")

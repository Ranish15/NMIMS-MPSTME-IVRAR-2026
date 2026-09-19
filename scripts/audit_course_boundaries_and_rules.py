import os
import sys
import re

# Ensure UTF-8 output on Windows
if sys.platform == "win32":
    import io
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding="utf-8")

FORBIDDEN_WORDS = [
    r"\bCSBS\b",
    r"\bComputer Science and Business Systems\b",
    r"\bArchana\b",
    r"\bBhise\b",
    r"\bSunny\b",
    r"\bNanade\b",
    r"\b702CO0E012\b",
    r"\b702TG0C003\b",
    r"Department of Information Technology",
    r"Institutional Leadership & Academic Directorate",
    r"\b70 Student\b"
]

CURRENCY_PATTERNS = [
    r"\\\$",                 # Escaped dollar like \$500
    r"₹",                    # Rupee symbol
    r"\bRs\b\.?\s*\d+",      # Rs. 500
    r"\bINR\b\s*\d+",        # INR 500
    r"\bUSD\b\s*\d+",        # USD 500
    r"\bEUR\b\s*\d+",        # EUR 500
    r"\brupees\b",           # rupees
    r"\bdollars\b"           # dollars
]

# Regex for emojis (excluding normal punctuation, math symbols, and box drawing lines)
EMOJI_PATTERN = re.compile(
    r"[\U0001F300-\U0001FAFF"  # Miscellaneous symbols and pictographs, emoticons
    r"\U00002600-\U000026FF"  # Miscellaneous symbols
    r"\U00002700-\U000027BF"  # Dingbats
    r"]", flags=re.UNICODE
)

def audit_file(filepath):
    # Skip binary, git, and python cache files
    ext = os.path.splitext(filepath)[1].lower()
    if ext in [".png", ".jpg", ".jpeg", ".pyc", ".ico", ".bin", ".pdf"]:
        return []
    if ".git" in filepath or os.path.sep + "scripts" + os.path.sep in filepath or filepath.startswith("scripts") or filepath.startswith(".\\scripts"):
        return []
        
    violations = []
    try:
        with open(filepath, "r", encoding="utf-8", errors="replace") as f:
            lines = f.readlines()
    except Exception as e:
        return [f"Could not read {filepath}: {e}"]

    for line_no, line in enumerate(lines, 1):
        # Check forbidden words
        for pattern in FORBIDDEN_WORDS:
            if re.search(pattern, line, re.IGNORECASE):
                violations.append(f"{filepath}:{line_no} [FORBIDDEN_TOKEN]: {line.strip()}")

        # Check currency symbols
        for curr in CURRENCY_PATTERNS:
            if re.search(curr, line, re.IGNORECASE):
                violations.append(f"{filepath}:{line_no} [CURRENCY_SYMBOL]: {line.strip()}")

        # Check emojis
        if EMOJI_PATTERN.search(line):
            violations.append(f"{filepath}:{line_no} [EMOJI_DETECTED]: {line.strip()}")

    return violations

def audit_group(group_folder):
    print(f"\n--- Running Boundary & Quality Audit on {os.path.basename(group_folder)} ---")
    all_violations = []
    file_count = 0
    
    for root, dirs, files in os.walk(group_folder):
        # Exclude git directories
        if ".git" in dirs:
            dirs.remove(".git")
        for f in files:
            fpath = os.path.join(root, f)
            violations = audit_file(fpath)
            if violations:
                all_violations.extend(violations)
            file_count += 1
            
    print(f"Audited {file_count} files in {os.path.basename(group_folder)}.")
    if all_violations:
        print(f"FAILED with {len(all_violations)} violations:")
        for v in all_violations[:20]:
            print("  " + v)
        if len(all_violations) > 20:
            print(f"  ... and {len(all_violations) - 20} more.")
        return False, all_violations
    else:
        print("PASSED: 100% Zero Defects (No CSBS, no forbidden names/codes, no currency symbols, no emojis).")
        return True, []

if __name__ == "__main__":
    if len(sys.argv) > 1:
        target = sys.argv[1]
        ok, violations = audit_group(target)
        sys.exit(0 if ok else 1)
    else:
        print("Usage: python audit_course_boundaries_and_rules.py <path_to_folder>")

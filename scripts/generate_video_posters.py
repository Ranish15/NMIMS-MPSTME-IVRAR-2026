import os
import json
import textwrap
from PIL import Image, ImageDraw, ImageFont

BASE_DIR = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

def get_fonts():
    try:
        font_header = ImageFont.truetype("segoeuib.ttf", 18)
        font_domain = ImageFont.truetype("segoeuib.ttf", 20)
        font_title = ImageFont.truetype("segoeuib.ttf", 22)
        font_sub = ImageFont.truetype("segoeui.ttf", 15)
        font_footer = ImageFont.truetype("segoeui.ttf", 15)
        font_footer_bold = ImageFont.truetype("segoeuib.ttf", 15)
    except Exception:
        try:
            font_header = ImageFont.truetype("arialbd.ttf", 18)
            font_domain = ImageFont.truetype("arialbd.ttf", 20)
            font_title = ImageFont.truetype("arialbd.ttf", 22)
            font_sub = ImageFont.truetype("arial.ttf", 15)
            font_footer = ImageFont.truetype("arial.ttf", 15)
            font_footer_bold = ImageFont.truetype("arialbd.ttf", 15)
        except Exception:
            font_header = ImageFont.load_default()
            font_domain = font_header
            font_title = font_header
            font_sub = font_header
            font_footer = font_header
            font_footer_bold = font_header
    return {
        "header": font_header,
        "domain": font_domain,
        "title": font_title,
        "sub": font_sub,
        "footer": font_footer,
        "footer_bold": font_footer_bold
    }

def generate_poster_for_group(group_folder, fonts):
    roster_path = os.path.join(group_folder, "docs", "TEAM_ROSTER.json")
    if not os.path.exists(roster_path):
        print(f"Skipping {os.path.basename(group_folder)}: no TEAM_ROSTER.json found")
        return

    with open(roster_path, "r", encoding="utf-8") as f:
        roster = json.load(f)

    group_id = roster.get("group_id", os.path.basename(group_folder))
    group_id_clean = group_id.replace("_", " ").upper()
    domain = roster.get("domain", "").strip().upper()
    title = roster.get("authorized_title", "")
    members = roster.get("members", [])

    # If domain is empty, create a clean domain summary from the title
    if not domain:
        # derive a short domain tag
        t_clean = title.split("impact")[0].split("enable")[0].split("mitigate")[0].split("for")[0]
        if "To what extent can" in t_clean:
            t_clean = t_clean.replace("To what extent can", "").strip()
        elif "How does" in t_clean:
            t_clean = t_clean.replace("How does", "").strip()
        domain = t_clean.strip().upper()
        if len(domain) > 60:
            domain = domain[:57] + "..."

    # Format student researchers list
    member_strs = [f"{m.get('name', '')} ({m.get('roll_no', '')})" for m in members]
    members_line = "Student Researchers: " + ", ".join(member_strs)

    # 16:9 Canvas (1280x720)
    width, height = 1280, 720
    bg_color = (9, 13, 22, 255)       # Rich dark slate
    card_color = (15, 23, 42, 255)    # Slate 900
    card_border = (30, 41, 59, 255)   # Slate 800
    accent_blue = (37, 99, 235, 255)  # Royal Blue
    cyan_text = (56, 189, 248, 255)   # Light Sky Cyan
    text_white = (248, 250, 252, 255) # White
    text_muted = (148, 163, 184, 255) # Slate 400
    text_light = (203, 213, 225, 255) # Slate 300

    img = Image.new("RGBA", (width, height), bg_color)
    draw = ImageDraw.Draw(img)

    # Outer Card Frame with Rounded Corners
    card_bbox = (32, 32, 1248, 688)
    draw.rounded_rectangle(card_bbox, radius=12, fill=card_color, outline=card_border, width=2)

    # Top Header
    header_text = "SVKM'S NMIMS MPSTME | IVRAR (702COI002) | SEMESTER V"
    draw.text((64, 56), header_text, fill=text_light, font=fonts["header"])

    # Header Separator Line
    draw.line([(64, 88), (1216, 88)], fill=accent_blue, width=2)

    # Domain / Group ID Subheader
    domain_text = f"{group_id_clean} \u2022 {domain}"
    # Truncate domain text if too wide
    if len(domain_text) > 85:
        domain_text = domain_text[:82] + "..."
    draw.text((64, 114), domain_text, fill=cyan_text, font=fonts["domain"])

    # Research Title (Wrapped)
    wrapped_title = textwrap.wrap(title, width=64)
    y_text = 160
    for line in wrapped_title[:4]:
        draw.text((64, y_text), line, fill=text_white, font=fonts["title"])
        y_text += 32

    # Play Button (Centered)
    btn_center_x, btn_center_y = 640, 415
    btn_radius = 44
    draw.ellipse(
        [
            (btn_center_x - btn_radius, btn_center_y - btn_radius),
            (btn_center_x + btn_radius, btn_center_y + btn_radius)
        ],
        fill=accent_blue
    )

    # White Play Triangle
    tri_points = [
        (btn_center_x - 8, btn_center_y - 18),
        (btn_center_x - 8, btn_center_y + 18),
        (btn_center_x + 18, btn_center_y)
    ]
    draw.polygon(tri_points, fill=text_white)

    # Walkthrough Subtitle below Play Button
    sub_text = "60-90s Interactive Simulation Walkthrough"
    bbox_sub = fonts["sub"].getbbox(sub_text)
    sub_w = bbox_sub[2] - bbox_sub[0]
    draw.text((btn_center_x - (sub_w // 2), btn_center_y + 54), sub_text, fill=text_muted, font=fonts["sub"])

    # Bottom Separator
    draw.line([(64, 610), (1216, 610)], fill=(51, 65, 85, 255), width=1)

    # Right Action Link: "Watch Demonstration on LinkedIn \u2192"
    action_text = "Watch Demonstration on LinkedIn \u2192"
    bbox_action = fonts["footer_bold"].getbbox(action_text)
    action_w = bbox_action[2] - bbox_action[0]
    action_x = 1216 - action_w
    draw.text((action_x, 632), action_text, fill=cyan_text, font=fonts["footer_bold"])

    # Left: Student Researchers (Clamped to avoid overlap)
    max_left_w = action_x - 90  # Keep 26px padding between left text and right action
    # Truncate student line if it exceeds max_left_w
    while fonts["footer"].getbbox(members_line)[2] > max_left_w and len(members_line) > 20:
        members_line = members_line[:-5] + "..."
    draw.text((64, 632), members_line, fill=text_light, font=fonts["footer"])

    # Save to group docs/figures/video_poster.png
    figures_dir = os.path.join(group_folder, "docs", "figures")
    os.makedirs(figures_dir, exist_ok=True)
    poster_path = os.path.join(figures_dir, "video_poster.png")
    img.save(poster_path, "PNG", optimize=True)
    print(f"Generated video poster: {os.path.basename(group_folder)}/docs/figures/video_poster.png")

def main():
    print(f"--- Generating Video Poster Cards for {os.path.basename(BASE_DIR)} ---")
    fonts = get_fonts()
    groups = sorted([d for d in os.listdir(BASE_DIR) if d.startswith("Group_") and os.path.isdir(os.path.join(BASE_DIR, d))])
    for g in groups:
        generate_poster_for_group(os.path.join(BASE_DIR, g), fonts)
    print(f"\nGenerated video posters for {len(groups)} groups.")

if __name__ == "__main__":
    main()

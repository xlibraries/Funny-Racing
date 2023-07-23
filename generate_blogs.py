import git
import markdown2

# Initialize the repository
repo = git.Repo(".")

# Create the blog content
blog_content = "# My Blog\n\n"

# Loop through all commits and fetch their descriptions
for commit in repo.iter_commits():
    commit_description = commit.message.strip()
    blog_content += f"## Commit {commit.hexsha}\n\n"
    blog_content += f"{commit_description}\n\n"

# Convert the blog content to HTML
html_content = markdown2.markdown(blog_content)

# Save the HTML content to the blog_post.html file
with open("blog_post.html", "w", encoding="utf-8") as html_file:
    html_file.write(html_content)

with open("index.html", "w", encoding="utf-8") as html_file:
    html_file.write(html_content)

print("Blog post generated successfully.")

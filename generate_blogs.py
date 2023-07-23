import git
import markdown2

# Initialize the repository
repo = git.Repo(".")

# Create a list to store the commit descriptions
commit_descriptions = []

# Loop through all commits and fetch their descriptions
for commit in repo.iter_commits():
    commit_descriptions.append(commit.message.strip())

# Create the blog content
blog_content = "# My Blog\n\n"

# Append each commit description as a separate section in the blog
for index, commit_desc in enumerate(commit_descriptions, 1):
    blog_content += f"## Commit {index}\n\n"
    blog_content += f"{commit_desc}\n\n"

# Convert the blog content to HTML
html_content = markdown2.markdown(blog_content)

# Save the HTML content to the blog_post.html file
with open("blog_post.html", "w", encoding="utf-8") as html_file:
    html_file.write(html_content)

print("Blog post generated successfully.")

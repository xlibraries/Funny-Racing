# generate_blogs.py

import os
import git
import markdown2

# Define the output directory for the blog posts
output_directory = "blogs"

def generate_blog_post(commit_hash, commit_message, code_diff):
    # Create a blog post file for each commit
    blog_filename = f"{output_directory}/{commit_hash}.md"

    # Compose the blog content with commit message and code changes
    blog_content = f"# Blog Post for Commit: {commit_hash}\n\n"
    blog_content += f"## Commit Message:\n\n{commit_message}\n\n"
    blog_content += "## Code Changes:\n\n```diff\n"
    blog_content += code_diff
    blog_content += "\n```"

    # Convert Markdown to HTML
    blog_html = markdown2.markdown(blog_content)

    # Write the HTML content to the blog post file
    with open(blog_filename, "w", encoding="utf-8") as f:
        f.write(blog_html)

if __name__ == "__main__":
    # Create the output directory if it doesn't exist
    if not os.path.exists(output_directory):
        os.makedirs(output_directory)

    # Initialize the Git repository
    repo = git.Repo(".")
    commits = list(repo.iter_commits())

    # Iterate through the commits and generate blog posts
    for commit in commits:
        commit_hash = commit.hexsha
        commit_message = commit.message.strip()
        code_diff = commit.diff(commit.parents[0]).__str__()

        generate_blog_post(commit_hash, commit_message, code_diff)

import os
from git import Repo
import markdown2

# Function to get the commit messages and generate HTML blog post content
def generate_blog_post(repo_path):
    repo = Repo(repo_path)
    commits = repo.iter_commits()
    
    blog_content = "# My Blog\n\n"
    
    for commit in commits:
        commit_sha = commit.hexsha
        commit_message = commit.message.strip()
        blog_content += f"## Commit {commit_sha}\n\n"
        blog_content += f"{commit_message}\n\n"
    
    return blog_content

# Main function
def main():
    repo_path = os.getcwd()  # Assuming the script is in the root directory of the repository

    # Generate blog post content from commit messages
    blog_content = generate_blog_post(repo_path)

    # Convert the blog post content to HTML
    html_content = markdown2.markdown(blog_content)

    # Save the HTML content to index.html file
    with open("index.html", "w", encoding="utf-8") as file:
        file.write(html_content)

if __name__ == "__main__":
    main()

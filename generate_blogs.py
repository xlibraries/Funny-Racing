import os
from git import Repo

# Function to get the commit messages and generate HTML blog post content
def generate_blog_post(repo_path):
    repo = Repo(repo_path)
    commits = repo.iter_commits()
    
    blog_content = ""
    
    for commit in commits:
        commit_sha = commit.hexsha
        commit_message = commit.message.strip()
        commit_html = f"<h2>Commit {commit_sha}</h2><p>{commit_message}</p>"
        
        blog_content += commit_html
    
    return blog_content

# Main function
def main():
    repo_path = os.getcwd()  # Assuming the script is in the root directory of the repository

    # Generate blog post content from commit messages
    blog_content = generate_blog_post(repo_path)

    # Generate the final HTML content
    blog_html = f"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="UTF-8">
        <title>My Blog</title>
    </head>
    <body>
        <h1>My Blog</h1>

        <!-- Blog post content -->
        {blog_content}
        <!-- End of blog post content -->
    </body>
    </html>
    """

    # Save the HTML content to index.html file
    with open("index.html", "w", encoding="utf-8") as file:
        file.write(blog_html)

if __name__ == "__main__":
    main()

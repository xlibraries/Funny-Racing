import git
import markdown2

# Function to convert Markdown to HTML
def convert_to_html(markdown_text):
    return markdown2.markdown(markdown_text)

# Get the commit message for the latest commit
repo = git.Repo(search_parent_directories=True)
commit = next(repo.iter_commits())

# Create the blog post content with commit message
blog_post_content = f"<h2>Commit {commit.hexsha}</h2>\n\n<p>{commit.message}</p>"

# Convert the blog post content to HTML
html_content = convert_to_html(blog_post_content)

# Save the HTML content to blog_post.html
with open("blog_post.html", "w", encoding="utf-8") as file:
    file.write(html_content)

print("Blog post generated successfully.")

import os
import sys

def generate_blog_post():
  """Generates a blog post from a file."""

  # Get the path to the blog post file.
  blog_post_file = os.path.join(os.getcwd(), "blog_post.md")

  # Read the content of the blog post file.
  with open(blog_post_file) as f:
    blog_post_content = f.read()

  # Generate the HTML content of the blog post.
  html_content = markdown.markdown(blog_post_content)

  # Write the HTML content of the blog post to a file.
  with open("blog_post.html", "w") as f:
    f.write(html_content)

if __name__ == "__main__":
  generate_blog_post()


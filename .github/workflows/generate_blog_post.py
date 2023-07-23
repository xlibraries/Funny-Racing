import git
import markdown2

# Initialize the Git repository
repo = git.Repo()

# Get the list of commits
commits = list(repo.iter_commits('main'))

# Initialize the Markdown converter
markdowner = markdown2.Markdown()

# Loop through each commit
for commit in commits:
    # Extract commit message
    commit_message = commit.message

    # Get the code diff for the commit
    code_diff = commit.diff(commit.parents[0]).__str__()

    # Convert the code diff to HTML
    pandoc_html = markdowner.convert(code_diff)

    # Add commit message to the beginning of the HTML content
    commit_html = f"<h2>Commit Message:</h2><p>{commit_message}</p>"
    pandoc_html_with_commit = commit_html + pandoc_html

    # Write the HTML content with the commit message to blog_post.html
    with open("blog_post.html", "w") as html_file:
        html_file.write(pandoc_html_with_commit)

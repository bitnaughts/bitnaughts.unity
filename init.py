import requests
import zipfile
import io
import os
import sys

REPO_URLS = ['bitnaughts', 'csharp.interpreter']


def getRepos():
    os.chdir(os.path.dirname(os.path.abspath(
        sys.path[0]) + "/bitnaughts/Assets/"))
    try:
        for repo in REPO_URLS:
            print("DOWNLOADING:\t" + repo)
            repo_zip_file = zipfile.ZipFile(io.BytesIO(requests.get(
                "https://github.com/bitnaughts/" + repo + "/archive/master.zip").content))
            print("EXTRACTING:\t" + repo)
            repo_zip_file.extractall()
        return True
    except Exception as e:
        print(e)
        return False


def main():
    if getRepos():
        print("INITIALIZATION COMPLETE!")
    else:
        print("Something went wrong. Submit an issue on our Github repository!")


if __name__ == "__main__":
    main()
    sys.exit()

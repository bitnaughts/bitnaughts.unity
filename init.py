import requests
import zipfile
import io
import os
import sys

REPO_URLS = ['https://github.com/bitnaughts/csharp.interpreter',
             'https://github.com/bitnaughts/bitnaughts', 'https://github.com/bitnaughts/mips.interpreter']


def getRepos():
    if REPO_URLS:
        # Look into Python environment independent way to achieve directory changes.
        # Alternatively, if the user has any issues, have the init.py file placed inside of /bitnaughts/Assets/ and omit the directory change (Lines 15-17).
        abs_path = os.path.abspath(sys.path[0])
        d_name = os.path.dirname(abs_path + '/bitnaughts/Assets/')
        os.chdir(d_name)
        try:
            for repo in REPO_URLS:
                print("DOWNLOADING:" + " " + repo)
                repo_zip_url = repo + "/archive/master.zip"
                get_repo_zip = requests.get(repo_zip_url)
                repo_zip_file = zipfile.ZipFile(
                    io.BytesIO(get_repo_zip.content))
                print("\tEXTRACTING " + repo + "....")
                repo_zip_file.extractall()
                print("\tDONE")
            return True
        except Exception as e:
            print(e)
            return False
    else:
        return "No repositories for download."


def main():
    status = getRepos()
    if status:
        print("You're all set!")
    else:
        print("Something went wrong.")


if __name__ == "__main__":
    main()
    sys.exit()

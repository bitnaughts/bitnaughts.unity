import requests
import zipfile
import io
import os
import sys

repos_url = "https://api.github.com/users/"
repo_zip_url = "https://github.com/"

def getRepos(username=None):
    print ("Getting repository list")
    try:
        url = repos_url + username + "/repos"
        response = requests.get(url)
        repos = response.json()
        return repos
    except Exception as e:
        print (e)
   
def saveRepos(username, repos=None):
    if repos:
        abspath = os.path.abspath(sys.path[0])
        dname = os.path.dirname(abspath + '/bitnaughts/Assets/')
        print (dname)
        os.chdir(dname)
        try:
            repo_number = 1
            for repo in repos:
                reponame = repo["name"]
                print (str(repo_number) + " " + reponame + " downloading....")
                repozipurl = repo_zip_url + username + "/" + reponame + "/archive/master.zip"
                get_repo_zip = requests.get(repozipurl)
                repozipfile = zipfile.ZipFile(io.BytesIO(get_repo_zip.content))
                print ("\tExtracting " + reponame + "....")
                repozipfile.extractall()
                print ("\t" + reponame + "downloading Complete :)")
                repo_number += 1
            return True
        except Exception as e:
            print (e)
            return False
    else:
        return "Don't have any repositories"


def main():
    username = 'bitnaughts'
    repos = getRepos(username)
    status = saveRepos(username=username, repos=repos)
    if status:
        print ("Done")
    else:
        print ("Something went wrong :(")

if __name__ == "__main__":
    main()
    sys.exit()
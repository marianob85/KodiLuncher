properties(
	[
		buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '', numToKeepStr: '10')),
		pipelineTriggers([pollSCM('0 H(5-6) * * *')])
	]
)

pipeline
{
	agent any
	options {
		skipDefaultCheckout true
	}
	stages
	{
		stage('Build'){
			agent{ label "windows/buildtools2019" }
			steps {
				checkout scm
				bat '''
					call "C:/BuildTools/VC/Auxiliary/Build/vcvars64.bat"
					nuget restore KodiLuncher.sln
					msbuild KodiLuncher.sln /t:Rebuild /p:Configuration=Release;Platform="Any CPU" /flp:logfile=warnings.log;warningsonly
				'''
				stash includes: "warnings.log", name: "warningsFiles"
				stash includes: '**/out/**/KodiLuncher.exe, **/out/**/KodiLuncher.pdb, **/out/**/*.dll', name: "bin"
			}
		}
		stage('Compile check'){
			steps {
				script {
					def warn = scanForIssues sourceCodeEncoding: 'UTF-8', tool: msBuild(id: 'msvc', pattern: 'warnings.log')
					publishIssues failedTotalAll: 1, issues: [warn], name: 'Win compilation warnings'	
				}
			}
		}
		
		stage('Archive'){
			steps {
				unstash "bin"
				archiveArtifacts artifacts: '*', fingerprint: true, onlyIfSuccessful: true
			}
		}
	}
	post { 
        changed { 
            emailext body: 'Please go to ${env.BUILD_URL}', to: '${DEFAULT_RECIPIENTS}', subject: "Job ${env.JOB_NAME} (${env.BUILD_NUMBER}) ${currentBuild.currentResult}".replaceAll("%2F", "/")
        }
    }
}
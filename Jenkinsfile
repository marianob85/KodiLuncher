properties(
	[
		buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '', numToKeepStr: '10')),
		pipelineTriggers([pollSCM('0 H(5-6) * * *')])
	]
)

pipeline
{
	agent { node { label 'windows10x64 && development' } }
	stages
	{
		stage('Build'){
			steps {
				bat '''
					call "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\Tools\\VsMSBuildCmd.bat"
					msbuild KodiLuncher.sln /t:Rebuild /p:Configuration=Release;Platform="Any CPU" /flp:logfile=warnings.log;warningsonly'''
			}
		}
		stage('Compile check'){
			steps {
				warnings canComputeNew: false, canResolveRelativePaths: false, defaultEncoding: '', excludePattern: '', healthy: '', includePattern: '', messagesPattern: '', parserConfigurations: [[parserName: 'MSBuild', pattern: 'warnings.log']], unHealthy: ''
			}
		}
		
		stage('Archive'){
			steps {
				archiveArtifacts artifacts: 'KodiLuncher/bin/Release/KodiLuncher.exe', caseSensitive: false, defaultExcludes: false, onlyIfSuccessful: true
			}
		}
		
		stage('CleanUp'){
			steps {
				deleteDir()
			}
		}
	}
	post { 
        failure { 
            notifyFailed()
        }
		success { 
            notifySuccessful()
        }
		unstable { 
            notifyFailed()
        }
    }
}

def notifySuccessful() {
	echo 'Sending e-mail'
	mail (to: 'notifier@manobit.com',
         subject: "Job '${env.JOB_NAME}' (${env.BUILD_NUMBER}) success build",
         body: "Please go to ${env.BUILD_URL}.");
}

def notifyFailed() {
	echo 'Sending e-mail'
	mail (to: 'notifier@manobit.com',
         subject: "Job '${env.JOB_NAME}' (${env.BUILD_NUMBER}) failure",
         body: "Please go to ${env.BUILD_URL}.");
}

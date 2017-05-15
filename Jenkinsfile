node("windows10x64 && development"){
	try {
		stage('Prepare'){
			checkout scm

		}
		stage('Build'){
			bat '''
				call "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\Tools\\VsMSBuildCmd.bat"
				msbuild KLuncher.sln /t:Rebuild /p:Configuration=Release;Platform="Any CPU" /flp:logfile=warnings.log;warningsonly'''
		}
		
		stage('Compile check'){
			warnings canComputeNew: false, canResolveRelativePaths: false, defaultEncoding: '', excludePattern: '', healthy: '', includePattern: '', messagesPattern: '', parserConfigurations: [[parserName: 'MSBuild', pattern: 'warnings.log']], unHealthy: ''
		}

		stage('Archive'){
			archiveArtifacts artifacts: 'KLuncher/bin/**.exe', onlyIfSuccessful: true
		}
		
		stage('CleanUp'){
			deleteDir()
			notifySuccessful()
		}
	}
	catch (e) {
		currentBuild.result = "FAILED"
		notifyFailed()
		throw e
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
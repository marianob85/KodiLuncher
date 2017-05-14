node("windows10x64 && development"){
	try {
		stage('Prepare'){
			checkout scm

		}
		stage('Build'){

		}

		stage('Archive'){
			// archiveArtifacts artifacts: 'src/*.exe', onlyIfSuccessful: true
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
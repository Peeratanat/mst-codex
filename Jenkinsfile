
def branchName
def imageName
def registry
def dockerCredentials
def projectName
def userEmail
def mailToRecipients
def approvalStatus
def okdCredentials
def okdUrl 
def okdDeploymentConfigs
def buildStatus
def catchError
def committerName
def projectType
def serviceName
def okdUATUrl
def currentBuildresult

build()
def build() {
    node  {
        try { 
			def appBackend    
			def scmVars = checkout scm
			branchName = scmVars.GIT_BRANCH
			serviceName = branchName.replace("prod-build-", "")
			serviceName = serviceName.replace("build-", "")
            registry = "apthailand/itproject"
            projectName = "apmicroservice" 
			projectNameDev = "ap-project-microservices" 
            dockerCredentials = "DOCKER_DEV"
            okdCredentials = 'OKD_DEV'
            okdDeploymentConfigs = "apcrm-$serviceName-service"
            catchError = ''
            projectType = 'API'
            stage('Clone repository') {         
                imageName = "$registry:$okdDeploymentConfigs-$BUILD_NUMBER"
                if (branchName.startsWith('build-')) {
                    okdUrl = 'https://api.dev.apthai.com:6443/'
                    okdUATUrl = 'https://api.uat.apthai.com:6443/'
                } 
				else if (branchName.startsWith('prod-build-'))
                {
                    buildStatus= '🍀🍀🍀🍀🍀🍀'      
                    currentBuildresult = "SUCCESS"
                    imageName = "$registry:prod-$okdDeploymentConfigs-$BUILD_NUMBER"
                }
				else
                {
                    throw new Exception("Branch ไม่ถูกต้อง")
                }
            }

            stage('Committer Name ') {
                committerName = sh(script: "git --no-pager log -1 --format='%an'",returnStdout: true).trim()
				println(committerName)
				committerName = committerName.replace('\\', '\\\\')
				println('replace')
                println(committerName)
            }
      

            stage('Build image') {
                sh 'echo "Build Image"'          
                 script {         
                    def files = sh(returnStdout: true, script: 'ls').trim()
                    echo "Files: ${files}"      
					sh "docker-compose -f docker-compose.yml build $okdDeploymentConfigs"
					sh "docker tag $okdDeploymentConfigs $imageName"
                }
            }

            stage('Push image') {
                 sh 'echo "Push image"'
                 withCredentials([usernamePassword(credentialsId: "$dockerCredentials", usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {    
                //  sh "ls"
                 sh "docker login -u$USERNAME -p$PASSWORD"
                //  sh "docker push $imageName"
                sh "docker push --disable-content-trust $imageName"
                 }    
            }

            stage('Remove Unused docker image') {    
                sh "docker rmi $imageName"  
            }  

            if(branchName == 'main'){
                buildStatus= '✅✅✅✅✅✅'  
            currentBuildresult = "SUCCESS" 
            } else if (branchName.startsWith('build-')) {
                deployDev()
				deployUAT()
            } else if (branchName == 'uat') {
                deployUAT()
            
			} else if (branchName == 'prod-build-') {
                buildStatus= '🍀🍀🍀🍀🍀🍀' 
				currentBuildresult = "SUCCESS"  
            }
        } catch (e) {
            buildStatus= '❌❌❌❌❌❌'  
        currentBuildresult = "FAILURE"  
            catchError = e.getMessage()
            throw e        
        } finally {
			if (branchName.startsWith('build-') || branchName.startsWith('prod-build-')) {
				notifyBuild()
			}
        }
    }
}


def deployDev() {
    try {
        stage('Login OKD Dev') {
            withCredentials([usernamePassword(credentialsId: "$okdCredentials", usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {   
                sh "oc login $okdUrl -u$USERNAME -p$PASSWORD"
                sh "oc project $projectNameDev"
                sh "oc patch deployment $okdDeploymentConfigs  --patch='{\"spec\":{\"template\":{\"spec\":{\"containers\":[{\"name\": \"$okdDeploymentConfigs\", \"image\":\"$imageName\"}]}}}}'"
            }
            buildStatus= '✅✅✅✅✅✅'
            currentBuildresult = "SUCCESS"
        }
    }
    catch(e) {
        buildStatus = '❌❌❌❌❌❌'
        currentBuildresult = "FAILURE"
        catchError = e.getMessage()
        throw e
    }
}

def deployUAT(){
    try {
        stage('Login OKD UAT') {
            script {
               withCredentials([usernamePassword(credentialsId: "$okdCredentials", usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {   
                sh "oc login $okdUATUrl -u$USERNAME -p$PASSWORD" 
                sh "oc project $projectName"
                sh "oc patch deployment $okdDeploymentConfigs  --patch='{\"spec\":{\"template\":{\"spec\":{\"containers\":[{\"name\": \"$okdDeploymentConfigs\", \"image\":\"$imageName\"}]}}}}'"
                }
            }
             buildStatus= '✅✅✅✅✅✅'
            currentBuildresult = "SUCCESS"
        }
    }
    catch(e) {
        buildStatus= '❌❌❌❌❌❌'
        currentBuildresult = "FAILURE"
        catchError = e.getMessage()
        throw e
    } 
}

def notifyBuild() {
 def discordWebhookUrl = 'https://discord.com/api/webhooks/1320945545002352663/VffaBlg6XDZbt_VnPOVX2hVQO-5EPgsjOhhHQoppvjLhviGTgcOgFaxNeazUDaGYxYqE'
    def color = currentBuildresult == "SUCCESS" ? 1148177 : 11278615
    def description = "Namespace :${projectName}\\nService : ${okdDeploymentConfigs}\\nImage : ${imageName}"
    def payload = """
    {
        "embeds": [
            {
                "title": "**CRM UpProgram**",
                "color": $color,
                "fields": [
                    {"name": "Build Status", "value": "$currentBuildresult", "inline": false},
                    {"name": "Description", "value": "${description}", "inline": false},
                    {"name": "Pusher", "value": "$committerName", "inline": false},
                    {"name": "Build URL", "value": "${BUILD_URL}", "inline": false}
                ]
            }
        ]
    }
    """
    sh """
    curl -X POST -H "Content-Type: application/json" -d '${payload}' ${discordWebhookUrl}
    """
}

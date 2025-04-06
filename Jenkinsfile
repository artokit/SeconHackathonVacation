pipeline {
    agent any
    environment {
        ENV_FILE = credentials('notification-service-env')
    }
    stages {
        stage('Checkout') {
            steps {
                git branch: 'master', url: 'https://github.com/artokit/SeconHackathonVacation.git'
            }
        }

        stage('Rebuild Docker') {
            steps {
                script {
                    sh 'cp $ENV_FILE ./notification-service/.env'
                    sh 'docker-compose up --build -d'
                }
            }
        }
    }
}
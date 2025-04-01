pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git branch: 'master', url: 'https://github.com/artokit/SeconHackathonVacation.git'
            }
        }

        stage('Rebuild Docker') {
            steps {
                script {
                    sh 'docker-compose down || true'
                    
                    sh 'docker-compose build --no-cache'
                    sh 'docker-compose up -d'
                }
            }
        }
    }
}
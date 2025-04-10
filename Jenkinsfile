pipeline {
    agent any
    environment {
        ENV_FILE = credentials('notification-service-env')
        FILE_SERVICE_ENV_FILE = credentials('file-service-env')
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
                    sh 'cp $ENV_FILE ~/temp.env'
                    sh 'mv ~/temp.env ./notification-service/.env'

                    sh 'cp $FILE_SERVICE_ENV_FILE ~/temp_file-service.env'
                    sh 'mv ~/temp_file-service.env ./file-service/.env'

                    sh 'docker-compose up --build -d'
                }
            }
        }
    }
}
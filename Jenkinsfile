pipeline {
    agent any

    environment {
        DOCKER_IMAGE = 'logging-service'
        DOCKER_TAG = "${env.BUILD_NUMBER}"
    }

    stages {
        stage('Restore Packages') {
            steps {
                echo 'Restoring...'
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo 'Building...'
                sh 'dotnet build --configuration Release --no-restore'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
                sh 'dotnet test --no-restore --verbosity normal'
            }
        }
        stage('Docker Build') {
            steps {
                sh "docker build -t ${DOCKER_IMAGE}:${DOCKER_TAG} ."
                sh "docker tag ${DOCKER_IMAGE}:${DOCKER_TAG} ${DOCKER_IMAGE}:latest"
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying...'
                sh 'docker-compose down || true'
                sh 'docker-compose up -d'
            }
        }
    }
}
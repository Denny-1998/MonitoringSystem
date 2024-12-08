pipeline {
    agent any

    environment {
        DOTNET_SYSTEM_GLOBALIZATION_INVARIANT = '1'
        DOCKER_IMAGE = 'logging-service'
        DOCKER_TAG = "${env.BUILD_NUMBER}"
        DOTNET_CLI = '/usr/local/dotnet/dotnet'  // Adjust this path based on where .NET is installed
    }

    stages {
        
        stage('Restore Packages') {
            steps {
                echo 'Restoring...'
                sh '/var/jenkins_home/.dotnet/dotnet restore ./LoggingService.sln'
            }
        }

        stage('Build') {
            steps {
                echo 'Building...'
                sh '/var/jenkins_home/.dotnet/dotnet build ./LoggingService.sln --configuration Release'
            }
        }
        stage('Test')
        {
            steps{
                echo 'Testing...'
                sh 'ls -la'  // List directory contents
                sh '/var/jenkins_home/.dotnet/dotnet test ./LoggingServiceTest/LoggingServiceTest.csproj'
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
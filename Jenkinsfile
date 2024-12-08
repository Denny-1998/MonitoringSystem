pipeline {
    agent any

    environment {
        DOTNET_SYSTEM_GLOBALIZATION_INVARIANT = '1'
        
    }

    stages {
        stage('Debug') {
            steps {
                echo 'Listing directory contents...'
                sh 'ls -la'
                sh 'pwd'  
            }
        }
        
        stage('Restore Packages') {
            steps {
                echo 'Restoring...'
                sh '/var/jenkins_home/.dotnet/dotnet restore ./MonitoringSystem/MonitoringSystem.sln'
            }
        }

        stage('Build') {
            steps {
                echo 'Building...'
                sh '/var/jenkins_home/.dotnet/dotnet build ./MonitoringSystem/MonitoringSystem.sln --configuration Release'
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
        
    }
}
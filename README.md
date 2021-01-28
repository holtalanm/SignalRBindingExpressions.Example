This project utilizes the azure signalr service emulator:

To Install the emulator:

    ```ps
    dotnet tool install  -g Microsoft.Azure.SignalR.Emulator --version 1.0.0-preview1-10692 --add-source https://www.myget.org/F/azure-signalr-dev/api/v3/index.json
    ```

    To run the emulator:

    ```ps
    asrs-emulator upstream init
    asrs-emulator start
    ```

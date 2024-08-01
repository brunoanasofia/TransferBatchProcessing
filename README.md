# TransferBatch Console Application

## Overview

The **TransferBatch** console application calculates commissions for bank account transfers using a CSV file with transfer data. 

The application computes the total commission for each account using the following rules:
- Accounts are charged a 10% commission on each transfer.
- The transfer with the highest value for each account on a given day is not subject to commission.

## Requirements

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later installed on your machine.

## Installation

1. Extract the Provided Solution:
- Download and extract the TransferBatchProcessing.zip file to a convenient location on your computer.

2. Open the Solution in Visual Studio:
- Open Visual Studio.
- Click on "Open a project or solution".
- Navigate to the folder where you extracted the solution and select the .sln file (solution file).

3. Build the Solution:
- In the Solution Explorer, right-click on the solution and select "Build Solution" or press Ctrl+Shift+B.
- Ensure that the build completes successfully. If there are any errors, review them in the Output window and address them accordingly.

4. Run the Application:
- Set the console application as the startup project by right-clicking on the project (usually the one containing the Main method) and selecting "Set as Startup Project".
- Press F5 to run the application in Debug mode or Ctrl+F5 to run it without debugging.

## Usage

The application expects a CSV file with the following structure:

<Account_ID>,<Transfer_ID>,<Total_Transfer_Amount>

Example CSV Input<br>
A10,T1000,100.00<br>
A11,T1001,100.00<br>
A10,T1002,200.00<br>
A10,T1003,300.00

## Running the Application

1. Input the File Path: When prompted, enter the path to your CSV file containing transfer data:<br>
![image](https://github.com/user-attachments/assets/fb0385f9-9ad5-475d-9e19-6a039e5dc7ce)



2. View the Output: After processing the file, the application outputs the total commission for each account.<br>
![image](https://github.com/user-attachments/assets/348e65c7-ed13-4b16-8914-4727d0e92755)


## Output Explanation

A10,30: Account A10 is charged a total commission of 30.<br>
A11,10: Account A11 is charged a total commission of 10.

## Handling Errors

- Invalid Transfer Amount: If a line in the CSV contains an invalid transfer amount, the application will display a warning and continue processing the remaining lines.<br>
![image](https://github.com/user-attachments/assets/dd155a65-46e0-4b72-a47e-ce06de1d42eb)


- Invalid Structure: If a line in the CSV file contains an invalid structure, such as using a comma instead of a dot (e.g., `A10,T1000,100,00`), 
the application will ignore this line, and continue processing the remaining lines.

- File Not Found: If the provided file path does not exist, the application will notify the user.<br>
![image](https://github.com/user-attachments/assets/1678c824-42ec-4233-b7b4-c6e30caae5ff)


## Code Structure

- Main Method: This is the entry point of the application. It handles user input and initiates file processing and commission calculation.
- ProcessFile Method: This method reads the CSV file and populates a dictionary of account transfers.
- CalculateCommissions Method: This method calculates the total commission for each account and prepares the output.

[Description]

This is a simple program that can be called from the terminal; it scans a spectrum specified for any signal strengths above a certain level.  Then reports that frequency to the standard output.

[Installation]

- [Requirements]
  * Expect must be installed
  * gqrx software must be installed, with the most recent drivers.

- Move the ScannerScript.sh and ScannerExpectScript.exp into a folder on the computer. (The ScannerScript.sh and ScannerExpectScript.exp must be in the same folder)

[Execution]

- open a terminal and navigate to the install folder containing the ScannerScript.sh and ScannerExpectScript.exp files. use the following syntax to execute the function:

  ./ScannerScript.sh 88900000 107900000 -20
                      ^         ^         ^
                      Starting Frequency  |
                                |         |
                                Ending Frequency
                                          |   
                                          Minimum signal strength required to count as a station in dB.

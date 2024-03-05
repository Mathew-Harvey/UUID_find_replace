
## UUID Transpose and Backup Tool Documentation

### Overview
This Command Line Interface (CLI) tool is designed to automatically find and transpose Universally Unique Identifiers (UUIDs) in .cs and .csv files within a specified directory, including all subdirectories. Prior to modification, it creates a comprehensive backup of the original directory state in a ZIP file. This document outlines the tool's functionality, usage guidelines, and its limitations.

### Features
- **UUID Transposition:** Modifies UUIDs in files according to specific transposition rules.
- **Backup Creation:** Automatically generates a backup ZIP file of the entire directory before making any changes.
- **Selective File Processing:** Only processes files with .cs or .csv extensions.

### Usage
To use this tool, navigate to the command line and execute the program with the desired directory path as the argument.

#### Syntax
```bash
dotnet run -- <directory-path>
```

#### Example
```bash
dotnet run -- C:\path\to\your\directory
```
> **Note:** Replace `<directory-path>` with the actual path to the directory you want to process.

### Backup Procedure
Upon execution, the tool first creates a backup of the specified directory. This backup includes all files and subdirectories, saved as `UUIDbackup.zip` in a `Backups` folder within the current working directory.

### Transposition Logic
UUIDs found within the specified files undergo character-wise transposition based on the following rules:
- Digits 0-8 are incremented by one (0 becomes 1, 1 becomes 2, etc.).
- The digit 9 is replaced with the letter a.
- Letters a-e are incremented by one (a becomes b, b becomes c, etc.).
- The letter f is replaced with the digit 0.

### Limitations
- **File Type Restriction:** Only files with .cs or .csv extensions are processed. Other file types within the directory are ignored.
- **Backup Overwrite Risk:** If the tool is run multiple times, newer backups will overwrite older ones in the Backups directory without warning. Users should manually manage backups to prevent data loss.
- **Manual Backup Removal:** Users must manually delete or move the backup ZIP file before running the program again to avoid unintentional overwrites.
- **UUID Format Assumption:** The tool assumes that UUIDs are in a standard format and may not correctly identify or transpose UUIDs that are part of larger strings or in non-standard formats.
- **Error Handling:** The tool provides basic error messages but does not implement advanced error recovery mechanisms. In the event of an error, manual intervention may be required.

### Best Practices
- **Verify Directory Contents:** Ensure the directory contains only the files you intend to modify, as the tool will process all matching files found.
- **Backup Management:** After running the tool, move the `UUIDbackup.zip` file to a secure location to prevent accidental overwrites in future runs.
- **Test Before Production Use:** Run the tool on a copy of your target directory first to verify that it behaves as expected before using it on critical or production data.

### Conclusion
This CLI tool offers a convenient solution for the automated transposition of UUIDs within specific file types, coupled with a robust backup feature. By understanding and adhering to the outlined usage guidelines and limitations, users can effectively leverage this tool in their development workflows.

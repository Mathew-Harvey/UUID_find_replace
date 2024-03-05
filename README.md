# UUID_find_replace
A cli app that will search through a directory and replace all uuids' in a predictable manner

## How It Works:
# TransposeUUID Method:
This method takes a UUID string and applies a character-wise transposition based on your mapping rules. It uses a helper function TransposeChar to convert each character according to the specified rules and then constructs a new string from the transformed characters.

# Mapping Logic:
The TransposeChar function handles the conversion of each character as per the rules:

This function only modifies hexadecimal characters (0-9, a-f). Non-hex characters (like the hyphens in a UUID) remain unchanged, ensuring the format of the UUID remains valid.
For digits 0 through 8, it simply increments them by 1.
For the digit 9, it changes it to a, transitioning from numeric to alphabetic hexadecimal representation.
For letters a through e, it increments them by 1 within the hexadecimal alphabetic range.
For the letter f, it wraps around to 0, completing the hexadecimal cycle.

# Printing the UUIDs:
For each found UUID, the original and the transposed UUID are printed side by side in the format originalUUID => transposedUUID.
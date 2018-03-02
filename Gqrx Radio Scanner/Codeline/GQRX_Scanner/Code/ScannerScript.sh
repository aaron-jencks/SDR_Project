freq=$1
Strength=0
while (( $freq < $2 )); do
	OUTPUT=$( expect ScannerExpectScript.exp $freq )
	printf -v Strength '%d\n' "$OUTPUT" 2>/dev/null
	until (( $Strength > $3 )); do
		let freq=$freq+200000
		OUTPUT=$( expect ScannerExpectScript.exp $freq )
		printf -v Strength '%d\n' "$OUTPUT" 2>/dev/null
	done
	echo "Found a station: " $freq
	let freq=$freq+200000
done
	
	

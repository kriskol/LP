#!/bin/bash
function TestProgram(){
echo "Testing assigment."
touch "tmpOriginal$3"
touch "tmpTest$3"
touch "tmpFileNames$3"

cat $2 | cut -d' ' -f1,2 > "tmpOriginal$3"
cat $2 | cut -d' ' -f1 > "tmpFileNames$3"


while read fileName; do
	echo "A"
	result = `cat $fileName | mono $1 $3 > "tmp$3.mod" && glpsol -m "tmp$3.mod" | grep 'OUTPUT:' | cut -d' ' -f2`
	if [ -z result]; then
		 result = "--"
	fi
	echo "$fileName $result" >> "tmpTest$3"
done < "tmpFileNames$3"
}

programPath=$1
rieseniePath=$2
assigment=$3

TestProgram $programPath $rieseniePath $assigment

diff "tmpOriginal$3" "tmpTest$3"

read resp

#rm tmp*

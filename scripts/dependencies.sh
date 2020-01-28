#!/bin/bash

set -e

if [ $# -eq 0 ]
then
    echo "This script needs to know at least the ITSME library version to proceed."
    echo "Sample usage: ./dependencies.sh 0.5.0.1563364026"
    echo "Optionally also supply the destination folder as a second parameter."
    exit 1
fi

VERSION=$1
LOCATION="."
EXTENSIONS=( "so" "dll" "dylib" )

if [ $# -eq 2 ]
then
    LOCATION=$2
    mkdir -p $LOCATION
    echo "Downloading files to: ${LOCATION}"
fi

for EXTENSION in "${EXTENSIONS[@]}"
do
    FILE_NAME="itsme_lib.${EXTENSION}"
    curl -o "${LOCATION}/${FILE_NAME}" "https://github.com/itsme-sdk/itsme-golang/releases/download/v${VERSION}/${FILE_NAME}" || exit 1
    echo "Successfully downloaded ${FILE_NAME}"
done

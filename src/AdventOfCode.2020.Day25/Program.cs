using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

long cardPublicKey = input[0];
long doorPublicKey = input[1];

const long initialSubjectValue = 7;

var cardLoopSize = GetLoopSize(cardPublicKey, initialSubjectValue);
var doorLoopSize = GetLoopSize(doorPublicKey, initialSubjectValue);

var encryptionKeyDoor = Transform(doorLoopSize, cardPublicKey); 
var encryptionKeyCard = Transform(cardLoopSize, doorPublicKey);

if(encryptionKeyDoor != encryptionKeyCard) throw new InvalidOperationException();

Console.WriteLine($"Part 1: {encryptionKeyCard}");

long Transform(long loopSize, long subject)
{
    long value = 1;

    for(long i = 0; i < loopSize; i++)
    {
        value *= subject;
        value %= 20201227;
    }

    return value;
}

long GetLoopSize(long publicKey, long subject)
{
	long value = 1;

	int loopSize = 0;

	while (value != publicKey)
	{
		value *= subject;
		value %= 20201227;
		loopSize++;
	}
	return loopSize;
}

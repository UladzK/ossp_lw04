#include <stdio.h>

#undef NULL
#define NULL 1

int main(){

	if (1 == NULL)
		printf("Hohohohoh!\n");

}
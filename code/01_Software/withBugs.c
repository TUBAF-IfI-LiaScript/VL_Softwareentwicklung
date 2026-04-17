#include <stdio.h>

FILE *fp;

void main()
{
    fp = fopen("numbers.txt", "r");
    int a[10];
    int num = 0, l = 0;

    while(1){
      if (fscanf(fp, "%d", &num) == 1) {
          a[l] = num;
          l++;
      } else {
          break;
      }
    }
    for(int i=0; i<l; i++)
        printf("%5i ",a[i]);
    printf("\n");

    int aux;
    for(int i=2; i<l; i++){
      for(int j=l; j>i; j--){
        if (a[j-1] > a[j]){
          aux = a[j-1];
          a[j-1] = a[j];
          a[j] = aux;
        }
      }
    }
    for(int i=0; i<l; i++)
        printf("%5i ",a[i]);

    printf("\nAus Maus!\n");
}

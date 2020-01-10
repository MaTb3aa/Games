#include<bits/stdc++.h>
#include <stdio.h>
#include <termios.h>
#include <unistd.h>
#include <fcntl.h>
#include <termios.h>
#include <stdio.h>

using namespace std;
int kbhit(void)
{
  struct termios oldt, newt;
  int ch;
  int oldf;

  tcgetattr(STDIN_FILENO, &oldt);
  newt = oldt;
  newt.c_lflag &= ~(ICANON | ECHO);
  tcsetattr(STDIN_FILENO, TCSANOW, &newt);
  oldf = fcntl(STDIN_FILENO, F_GETFL, 0);
  fcntl(STDIN_FILENO, F_SETFL, oldf | O_NONBLOCK);

  ch = getchar();

  tcsetattr(STDIN_FILENO, TCSANOW, &oldt);
  fcntl(STDIN_FILENO, F_SETFL, oldf);

  if(ch != EOF)
  {
    ungetc(ch, stdin);
    return 1;
  }

  return 0;
}
int getch(void)
{
	struct termios oldt, newt;
	int ch;

	tcgetattr(STDIN_FILENO, &oldt);
	newt = oldt;
	newt.c_lflag &= ~(ICANON | ECHO);
	tcsetattr(STDIN_FILENO, TCSANOW, &newt);
	ch = getchar();
	tcsetattr(STDIN_FILENO, TCSANOW, &oldt);

	return ch;
}

bool gameOver;
const int width=20;
const int height=20;
int x,y,fruitX,fruitY,Score;
enum eDirection {Stop = 0 ,Left ,Right ,Up ,Down};
eDirection dir;
void Setup(){
    gameOver= false ;
    dir =Stop;
    x=width / 2 ;
    y= height / 2 ;
    fruitX = rand() % width;
    fruitY = rand() % height;
    Score = 0 ;
}
void Draw(){
    system ("clear");
    for(int i=0;i<width+2;i++)cout<<"#";
    cout<<endl;
    for(int i=0;i<height;i++){
        for(int j=0;j<width;j++){
            if(j==0)cout<<"#";
            if(i==x&&j==y)cout<<"O";
            else if(i==fruitX&&j==fruitY)cout<<"F";
            else cout<<" ";
            if(j==width-1)cout<<"#";

        }
        cout<<endl;
    }

    for(int i=0;i<width+2;i++)cout<<"#";
    cout<<endl;
}

void Input(){

    if(kbhit()){
        switch(getch()){
            case 'a':
                dir=Left;
                break;
            case 'd':
                dir=Right;
                break;
             case 'w':
                dir=Up;
                break;
            case 's':
                dir=Down;
                break;
            case 'x':
                gameOver=true;
                break;
        }

    }

}
void Logic(){

          switch(dir){
            case Left:
                x--;
                break;
            case Right:
                x++;
                break;
             case Up :
                y--;
                break;
            case Down :
                y++;
                break;
            default :
                break;
        }
}
int main() {


    Setup();
    while(!gameOver){

        Draw();
        Input();
        Logic();
      //  sleep(.8);
    }
    return	0;
}

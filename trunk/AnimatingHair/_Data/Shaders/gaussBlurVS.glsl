﻿varying vec2 vTexCoord;
 
void main(void)
{
   gl_Position = ftransform();;
  
   vTexCoord = gl_MultiTexCoord0.st;
}
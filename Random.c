/*	example code for cc65, for NES
 *  construct some sprites - put 64 sprites in random positions
 *	using neslib
 *	Doug Fraker 2018
 */	

 
 
#include "LIB/neslib.h"
#include "LIB/nesdoug.h"
#include "Sprites.h" // holds our metasprite data



#pragma bss-name(push, "ZEROPAGE")

// GLOBAL VARIABLES
unsigned char sprid; // remember the index into the sprite buffer
unsigned char pad1;
unsigned char start_pressed;
unsigned char index;


#pragma bss-name(push, "BSS")

unsigned char spr_x[64];
unsigned char spr_y[64];


const unsigned char text[]="Press Start";

const unsigned char palette_bg[]={
0x0f, 0x00, 0x10, 0x30, // black, gray, lt gray, white
0,0,0,0,
0,0,0,0,
0,0,0,0
}; 

const unsigned char palette_sp[]={
0x0f, 0x0f, 0x0f, 0x28, // black, black, yellow
0,0,0,0,
0,0,0,0,
0,0,0,0
}; 


	

	
	
void main (void) {
	
	ppu_off(); // screen off
	
	// load the palettes
	pal_bg(palette_bg);
	pal_spr(palette_sp);

	// use the second set of tiles for sprites
	// both bg and sprite are set to 0 by default
	bank_spr(1);

	// load the text
	// vram_adr(NTADR_A(x,y));
	vram_adr(NTADR_A(7,14)); // set a start position for the text
	
	// vram_write draws the array to the screen
	vram_write(text,sizeof(text));
	
	ppu_on_all(); // turn on screen
	
	
	
	while (1){
		// infinite loop

		ppu_wait_nmi(); // wait till beginning of the frame
		// the sprites are pushed from a buffer to the OAM during nmi
		
		oam_clear();
		sprid = 0;
		
		pad1 = pad_poll(0); // read the first controller
		if(!start_pressed){
			if(pad1 & PAD_START){
				++start_pressed;
				seed_rng(); // the frame count was ticking up every frame till Start pressed
				for(index=0;index<64;++index){
					spr_x[index] = rand8(); //fill the arrays with random #s
					spr_y[index] = rand8();
				}
			}
		}
		else{ // fill the sprite buffer
			for(index=0;index<25;++index){
				if(get_frame_count() & 1){ // half the time
					spr_y[index] = spr_y[index] + 1; // fall
				}
				sprid = oam_spr(spr_x[index], spr_y[index], 0, 0, sprid);
			}
			for( ;index<55;++index){
				spr_y[index] = spr_y[index] + 1; // fall
				sprid = oam_spr(spr_x[index], spr_y[index], 0, 0, sprid);
			}
			for( ;index<64;++index){
				spr_y[index] = spr_y[index] + 2; // fall fast
				sprid = oam_spr(spr_x[index], spr_y[index], 0, 0, sprid);
			}
		}
	}
}
	
	
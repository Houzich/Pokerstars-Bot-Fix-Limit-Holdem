
#ifndef __KEYBOARD_H
#define __KEYBOARD_H


  struct keyboardHID_t {
      uint8_t id;
      uint8_t modifiers;
      uint8_t key1;
      uint8_t key2;
      uint8_t key3;
  };
  extern struct keyboardHID_t keyboardHID;	
	
	// USB keyboard codes 
#define USB_HID_MODIFIER_LEFT_CTRL   0x01 
#define USB_HID_MODIFIER_LEFT_SHIFT  0x02 
#define USB_HID_MODIFIER_LEFT_ALT    0x04 
#define USB_HID_MODIFIER_LEFT_GUI    0x08 // (Win/Apple/Meta) 
#define USB_HID_MODIFIER_RIGHT_CTRL  0x10 
#define USB_HID_MODIFIER_RIGHT_SHIFT 0x20 
#define USB_HID_MODIFIER_RIGHT_ALT   0x40 
#define USB_HID_MODIFIER_RIGHT_GUI   0x80 
#define USB_HID_KEY_L     0x0F 
	
#define USB_HID_KEY_1     30 	
#define USB_HID_KEY_2     31	
#define USB_HID_KEY_3     32	
#define USB_HID_KEY_4     33	
#define USB_HID_KEY_5     34	
#define USB_HID_KEY_6     35	
#define USB_HID_KEY_7     36	
#define USB_HID_KEY_8     37	
#define USB_HID_KEY_9     38	
#define USB_HID_KEY_0     39	
#define USB_HID_KEY_RIGHTARROW		79
#define USB_HID_KEY_LEFTARROW			80
#define USB_HID_KEY_DOWNARROW			81
#define USB_HID_KEY_UPARROW				82
#define USB_HID_KEY_SPACEBAR			44

void USB_Send_Key(uint8_t key);	
void Keyboard_Init(void);
	
#endif /* __KEYBOARD_H */


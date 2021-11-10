
/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "stm32f3xx_hal.h"
#include "string.h"
#include "usbd_core.h"
#include "usbd_desc.h"
#include "usbd_hid.h" 
#include "usbd_def.h"
#include "usb_device.h"
#include "Keyboard.h"

/* Private variables ---------------------------------------------------------*/
struct keyboardHID_t keyboardHID;
USBD_HandleTypeDef USBD_Device;

void Keyboard_Init(void)
{
  keyboardHID.id = 1;
  keyboardHID.modifiers = 0;
  keyboardHID.key1 = 0;
  keyboardHID.key2 = 0;
  keyboardHID.key3 = 0;
	
  /* Init Device Library */
  USBD_Init(&USBD_Device, &FS_Desc, 0);
  
  /* Register the HID class */
  USBD_RegisterClass(&USBD_Device, &USBD_HID);
  
  /* Start Device Process */
  USBD_Start(&USBD_Device);	
}

void USB_Send_Key(uint8_t key)
{
    keyboardHID.modifiers = 0;
    keyboardHID.key1 = key;
    USBD_HID_SendReport(&USBD_Device, (uint8_t *)&keyboardHID, sizeof(struct keyboardHID_t));
    HAL_Delay(30);
    keyboardHID.modifiers = 0;
    keyboardHID.key1 = 0;
    USBD_HID_SendReport(&USBD_Device, (uint8_t *)&keyboardHID, sizeof(struct keyboardHID_t));	
}



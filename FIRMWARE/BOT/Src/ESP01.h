
#ifndef __ESPO1_H
#define __ESPO1_H



typedef enum 
{
  ESPO1_COMMAND_OK       = 0x00U,
  ESPO1_COMMAND_ERROR    = 0x01U,
} ESP01_StatusTypeDef;


ESP01_StatusTypeDef ESP01_Send(char *transmit_str, char *receive_str1,  char *receive_str2);
ESP01_StatusTypeDef ESP01_Send_Command(char *transmit_str);
ESP01_StatusTypeDef ESP01_Receive(char *receive_str1, char *receive_str2);
void ESP01_Send_Response_OK(void);
void ESP01_Init(void);

	extern char ESP01_Receive_Buff[];
	extern char ESP01_Receive_Text[];
#endif /* __ESPO1_H */


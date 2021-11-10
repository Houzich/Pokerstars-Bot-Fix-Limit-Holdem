
/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "stm32f3xx_hal.h"
#include "string.h"
#include "ESP01.h"

/* Private variables ---------------------------------------------------------*/
extern UART_HandleTypeDef huart2;

	char ESP01_Receive_Buff[200];
	char ESP01_Receive_Text[200];


void ESP01_Init(void)
{

	
	HAL_GPIO_WritePin(ESP01_Reset_GPIO_Port, ESP01_Reset_Pin, GPIO_PIN_RESET);
	HAL_Delay(100);
	HAL_GPIO_WritePin(ESP01_Reset_GPIO_Port, ESP01_Reset_Pin, GPIO_PIN_SET);
	HAL_Delay(500);
	
ESP01_Send_Command("AT+CWMODE=3\r\n");	
ESP01_Send_Command("AT+RST\r\n");
HAL_Delay(1000);
ESP01_Send_Command("AT+CWJAP=\"685aec\",\"271320229\"\r\n");	
ESP01_Send_Command("AT+CIPSTA=\"192.168.0.199\"\r\n");	
ESP01_Send_Command("AT+CIPMUX=1\r\n");	
ESP01_Send_Command("AT+CIPSERVER=1,8080\r\n");	
ESP01_Send_Command("AT+CIFSR\r\n");

//AT+CIPSTA="192.168.0.199","192.168.0.1","255.255.255.0"
}

ESP01_StatusTypeDef ESP01_Send_Command(char *transmit_str)
{
	return ESP01_Send(transmit_str,"OK\r\n","ERROR\r\n");	
}

ESP01_StatusTypeDef ESP01_Send(char *transmit_str, char *receive_str1,  char *receive_str2)
{
	char str[200] = {0x00};
	volatile char *p_str = &str[0];
	
	for(int i=0; i<sizeof(str); i++) str[i]=0;
	strcpy(str, (char *)transmit_str); 
	
	
			printf("\r\nTRANSMIT:\r\n%s", str);
			HAL_UART_AbortReceive_IT(&huart2);
			READ_REG(huart2.Instance->RDR);
			HAL_UART_AbortTransmit(&huart2);
			if(HAL_UART_Transmit(&huart2, (uint8_t *)str, strlen(str),10)==HAL_OK)
			{
				str[0]=1;
			}
			else
			{
				str[0]=2;
			}
			for(int i=0; i<sizeof(str); i++) str[i]=0;
			p_str = str;			
			HAL_UART_Receive_IT(&huart2, (uint8_t *)p_str, sizeof(str));
			do
				{
				}	
			while((!strstr(str,receive_str1))&&(!strstr(str,receive_str2)));	
			HAL_UART_AbortReceive_IT(&huart2);				
				printf("\r\nRECEIVE:\r\n%s", str);	
				//HAL_Delay(1000);
				if(strstr(str,receive_str2)){
					return ESPO1_COMMAND_ERROR;
				}
				else {
					return ESPO1_COMMAND_OK;
				}
}


ESP01_StatusTypeDef ESP01_Receive(char *receive_str1, char *receive_str2)
{
				if(ESP01_Receive_Buff[0]==0)
				{
					HAL_UART_AbortReceive_IT(&huart2);
					HAL_UART_Receive_IT(&huart2, (uint8_t *)&ESP01_Receive_Buff[0], sizeof(ESP01_Receive_Buff));
				}
	
			do
				{
				}	
			while((!strstr(ESP01_Receive_Buff,receive_str1))&&(!strstr(ESP01_Receive_Buff,receive_str2)));
					strcpy(ESP01_Receive_Text,ESP01_Receive_Buff);
					for(int i=0; i<sizeof(ESP01_Receive_Buff); i++) ESP01_Receive_Buff[i]=0;
					HAL_UART_AbortReceive_IT(&huart2);
					HAL_UART_Receive_IT(&huart2, (uint8_t *)&ESP01_Receive_Buff[0], sizeof(ESP01_Receive_Buff));
				
				printf("\r\nRECEIVE:\r\n%s", ESP01_Receive_Text);	
				return ESPO1_COMMAND_OK;
}

void ESP01_Send_Response_OK(void)
{
		if(ESP01_Send("AT+CIPSEND=0,12\r\n","\r\n>","ERROR\r\n")==ESPO1_COMMAND_ERROR)return;
		if(ESP01_Send("<h1>OK!</h1>\r\n","OK\r\n","ERROR\r\n")==ESPO1_COMMAND_ERROR)return;
}

#include "stm32f4xx_conf.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_usart.h"
#include "misc.h"
#include "stm32f4xx_tim.h"
#include "stm32f4xx_exti.h"
#include "stm32f4xx_syscfg.h"

void send_string(const char* s);

int buffer[5];
int i=0;

int main(void)
{
	//TIMER
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	TIM_TimeBaseStructure.TIM_Period =1000-1;
	TIM_TimeBaseStructure.TIM_Prescaler = 8400-1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);
	TIM_Cmd(TIM3, ENABLE);

	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_1);
	NVIC_InitTypeDef NVIC_InitStructure;
	NVIC_InitStructure.NVIC_IRQChannel=TIM3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority=0x00;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority=0x00;
	NVIC_InitStructure.NVIC_IRQChannelCmd=ENABLE;
	NVIC_Init(&NVIC_InitStructure);
	TIM_ClearITPendingBit(TIM3,TIM_IT_Update);
	TIM_ITConfig(TIM3,TIM_IT_Update,ENABLE);

	//USART
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART3, ENABLE);

	GPIO_InitTypeDef GPIO_InitStructure;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10 | GPIO_Pin_11;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	GPIO_PinAFConfig(GPIOC, GPIO_PinSource10, GPIO_AF_USART3);
	GPIO_PinAFConfig(GPIOC, GPIO_PinSource11, GPIO_AF_USART3);

	USART_InitTypeDef USART_InitStructure;
	USART_InitStructure.USART_BaudRate = 9600;
	USART_InitStructure.USART_StopBits = USART_StopBits_1;
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_InitStructure.USART_Parity = USART_Parity_No;
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	USART_Init(USART3, &USART_InitStructure);
	USART_Cmd(USART3, ENABLE);


	NVIC_InitStructure.NVIC_IRQChannel = USART3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);
	NVIC_EnableIRQ(USART3_IRQn);


	//PRZYCISK
	NVIC_InitStructure.NVIC_IRQChannel = EXTI0_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x00;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x00;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

	EXTI_InitTypeDef EXTI_InitStructure;
	EXTI_InitStructure.EXTI_Line = EXTI_Line0;
	EXTI_InitStructure.EXTI_Mode = EXTI_Mode_Interrupt;
	EXTI_InitStructure.EXTI_Trigger = EXTI_Trigger_Rising;
	EXTI_InitStructure.EXTI_LineCmd = ENABLE;
	EXTI_Init(&EXTI_InitStructure);

	SYSCFG_EXTILineConfig(GPIOD, EXTI_PinSource1);

    while(1)
    {

    if (USART_GetFlagStatus(USART3, USART_FLAG_RXNE)) {
    	char c = USART_ReceiveData(USART3);
    	if(i>=5)
    		i=0;

    		if(c!='g') { 	 //g testowo jako koniec przesylania.
    			buffer[i] = c;
    			i++;
    	            }


    	     if(c == 'g')
    	     	 {
    	    	  send_string("\n");
    	          send_string(buffer);
    	          i=0;
    	          }
    													}
    	       }


    }

//Timer dla przycisku
void TIM3_IRQHandler(void)

{
        if(TIM_GetITStatus(TIM3,TIM_IT_Update)!=RESET)
        {
        	//czekaj na opró¿nienie bufora wyjœciowego
        	while(USART_GetFlagStatus(USART3, USART_FLAG_TXE) == RESET);
        	// wyslanie danych
        	send_string("TEST\n");
        	// czekaj az dane zostana wyslane
        	while (USART_GetFlagStatus(USART3, USART_FLAG_TC) == RESET);
            TIM_ClearITPendingBit(TIM3,TIM_IT_Update);
            TIM_Cmd(TIM3, DISABLE);
        }

}

//USART
void USART3_IRQHandler(void)
{
	// sprawdzenie flagi zwiazanej z odebraniem danych przez USART
	if(USART_GetITStatus(USART3, USART_IT_RXNE) != RESET)
       {
		// odebrany bajt znajduje sie w rejestrze USART3->DR
	}
}

//Wysylanie znaku
void send_char(char c)
{
    while (USART_GetFlagStatus(USART3, USART_FLAG_TXE) == RESET);
    USART_SendData(USART3, c);
}

//Wysylanie stringa
void send_string(const char* s)
{
    while (*s)
        send_char(*s++);
}

//Wysylanie stringa2
void UARTSend(const unsigned char *pucBuffer, unsigned long ulCount)
{
    //
    // Loop while there are more characters to send.
    //
    while(ulCount--)
    {
        USART_SendData(USART3, (uint16_t) *pucBuffer++);
        /* Loop until the end of transmission */
        while(USART_GetFlagStatus(USART3, USART_FLAG_TC) == RESET)
        {
        }
    }
}

//PRZERWANIE PRZYCISKU
void EXTI0_IRQHandler(void) {
	if (EXTI_GetITStatus(EXTI_Line0) != RESET) {

		TIM_Cmd(TIM3, ENABLE);
		EXTI_ClearITPendingBit(EXTI_Line0);
	}
}

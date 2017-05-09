#include "defines.h"
#include "tm_stm32f4_hd44780.h"
#include "stm32f4xx.h"
#include "stm32f4xx_usart.h"
#include "stm32f4xx_conf.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_exti.h"
#include "misc.h"
#include "stm32f4xx_tim.h"
#include "stm32f4xx_syscfg.h"
 
int main(void)
{
    SystemInit();
 
    // wlaczenie taktowania wybranego portu
    RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);
 
    // wlaczenie taktowania wybranego uk�adu USART
    RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART3, ENABLE);
 
    // konfiguracja linii Rx i Tx
    GPIO_InitTypeDef GPIO_InitStructure;
    GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10 | GPIO_Pin_11;
    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
    GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
    GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
    GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
    GPIO_Init(GPIOC, &GPIO_InitStructure);
    // ustawienie funkcji alternatywnej dla pin�w (USART)
    GPIO_PinAFConfig(GPIOC, GPIO_PinSource10, GPIO_AF_USART3);
    GPIO_PinAFConfig(GPIOC, GPIO_PinSource11, GPIO_AF_USART3);
 
    //konfiguracja USART
    USART_InitTypeDef USART_InitStructure;
    // predkosc transmisji (mozliwe standardowe opcje: 9600, 19200, 38400, 57600, 115200, ...)
    USART_InitStructure.USART_BaudRate = 115200;
    // d�ugo�� s�owa (USART_WordLength_8b lub USART_WordLength_9b)
    USART_InitStructure.USART_WordLength = USART_WordLength_8b;
    // liczba bit�w stopu (USART_StopBits_1, USART_StopBits_0_5, USART_StopBits_2, USART_StopBits_1_5)
    USART_InitStructure.USART_StopBits = USART_StopBits_1;
    // sprawdzanie parzysto�ci (USART_Parity_No, USART_Parity_Even, USART_Parity_Odd)
    USART_InitStructure.USART_Parity = USART_Parity_No;
    // sprz�towa kontrola przep�ywu (USART_HardwareFlowControl_None, USART_HardwareFlowControl_RTS, USART_HardwareFlowControl_CTS, USART_HardwareFlowControl_RTS_CTS)
    USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
    // tryb nadawania/odbierania (USART_Mode_Rx, USART_Mode_Rx )
    USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
 
    // konfiguracja
    USART_Init(USART3, &USART_InitStructure);
    // wlaczenie ukladu USART
    USART_Cmd(USART3, ENABLE);
 
    // konfiguracja NVIC
    //struktura do konfiguracji kontrolera NVIC
    NVIC_InitTypeDef NVIC_InitStructure;
    // wlaczenie przerwania zwi�zanego z odebraniem danych (pozostale zrodla przerwan zdefiniowane sa w pliku stm32f4xx_usart.h)
    USART_ITConfig(USART3, USART_IT_RXNE, ENABLE);
    NVIC_InitStructure.NVIC_IRQChannel = USART3_IRQn;
    NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
    NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
    NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
    // konfiguracja kontrolera przerwan
    NVIC_Init(&NVIC_InitStructure);
    // wlaczenie przerwan od ukladu USART
    NVIC_EnableIRQ(USART3_IRQn);
 
    //Clear LCD
    TM_HD44780_Clear();
 
    //Initialize LCD 16 cols x 2 rows
    TM_HD44780_Init(16, 2);
    char init[] = "Initializing...";
    //Put string to LCD
    TM_HD44780_Puts(0, 0, init);
 
    Delayms(500);
 
    TM_HD44780_Clear();
 
    /*//czekaj na opr�nienie bufora wyj�ciowego
    while(USART_GetFlagStatus(USART3, USART_FLAG_TXE) == RESET);
    // wyslanie danych
    USART_SendData(USART3, 'A');
    // czekaj az dane zostana wyslane
    while (USART_GetFlagStatus(USART3, USART_FLAG_TC) == RESET);*/
 
 
    while(1){
 
 
    }
}
void USART3_IRQHandler(void)
{
    // sprawdzenie flagi zwiazanej z odebraniem danych przez USART
    if(USART_GetITStatus(USART3, USART_IT_RXNE) != RESET)
    {
        // odebrany bajt znajduje sie w rejestrze USART3->DR
        while (USART_GetFlagStatus(USART3, USART_FLAG_RXNE) == RESET);
        uint8_t receive = USART_ReceiveData(USART3);
        while(USART_GetFlagStatus(USART3, USART_FLAG_TXE) == RESET);
        // wyslanie danych
        USART_SendData(USART3, receive);
        TM_HD44780_Puts(0,0,receive);
        // czekaj az dane zostana wyslane
        while (USART_GetFlagStatus(USART3, USART_FLAG_TC) == RESET);
 
    }
}
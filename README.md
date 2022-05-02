# Controle Estacionamento

Os valores praticados pelo estacionamento devem ser parametrizados em uma tabela de
preços com controle vigência, o estacionamento fica aberto 24 horas, dessa forma poderão 
existir carros que ficarão estacionados de um dia para outro, e ou estacionados por mais 
dias. 

## Exemplo: 
- Valores válidos para o período de 01/01/2022 até 31/12/2022.
- Deverá ser parametrizado também horários em que o estácionamento ficará ‘livre’, por padrão 
segundas,quartas e quintas entre as 11:30 e 13:00.

Utilizar a data de entrada do veículo como referência para buscar a tabela de preços

A tabela de preço deve contemplar o valor da hora inicial e valor para as horas adicionais.

# Demo
- Será cobrado metade do valor da hora inicial quando o tempo total de permanência no
estacionamento for igual ou inferior a 30 minutos.
- O valor da hora adicional possui uma tolerância de 10 minutos para cada 1 hora.

Exemplo:
- 30 minutos valor R$ 1,00
- 1 hora valor R$ 2,00
- 1 hora 10 minutos valor R$ 2,00
- 1 hora e 15 minutos valor R$ 3,00
- 2 horas e 5 minutos valor R$ 3,00
- 2 horas e 15 minutos valor R$ 4,00

Utilizar a placa do veículo como chave de busca.

Bônus:
- Cadastro opcional de condutor durante o pagamento, para cada 10 horas estacionas, o 
condutor terá um desconto de 50% nas proximas duas vezes que for estacionar. Horas 
de estacionamento dentro do periodo ‘livre’ não devem contar para o bonus
- Histórico de estacionamento, por condutor e por veiculo.
- Histórico de veiculos já finalizados, no dia ou dias anterires.
- Consulta de veiculos que ainda estão em abertos.

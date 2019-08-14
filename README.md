# kafka-demo


## Confluent
Kafka was started at linkedin, however several of the people who started it have started a new company  [Confluent](https://www.confluent.io/) around it. [Confluent Github](https://github.com/confluentinc) is the currently the best plcae for kafka tools / libraries.

## docker-compose
It is tricky to get kafka and zookeeper working locally. Lots of the examples on the web only work on linux or have issues. This [docker-compose](https://github.com/simplesteph/kafka-stack-docker-compose/blob/master/zk-single-kafka-single.yml) appears to be the best/simplest one available.


## Kafka Tool
kafka tool is a great way to explore kafka. Get version 2 [Kafka Tool](http://www.kafkatool.com/download.html) 

### Add the cluster
![add the cluster](docs/add-cluster.png)


## C# Library
The confluent C# library is on [github](https://github.com/confluentinc/confluent-kafka-dotnet). It was recently overhauled so lots (most) of the examples of how to use it on the web are wrong. The examples in the repo are the best starting point.



1. ##### 安装gcc环境

   ```
   yum install gcc
   ```

2. Redis下载

   官网下载地址：https://redis.io/download

   ```
   cd /usr/local
   wget http://download.redis.io/releases/redis-5.0.5.tar.gz
   tar -zxvf redis-5.0.5.tar.gz -C /usr/local/
   cd redis-5.0.5
   ```

3. 编译

   ```
   make & make install
   ```

4. 创建节点

   ```
   mkdir /usr/local/redis-cluster
   cd /usr/local/redis-cluster
   mkdir 7000 7001 7002 7003 7004 7005
   cp /usr/local/redis-5.0.5/redis.conf /usr/local/redis-cluster/7000
   ```

5. 修改配置文件，先修改7000节点配置，其它节点复制7000配置内容

   ```
   bind 0.0.0.0       //开启网络，保证外部可连接
   port 7000         //每个节点的端口号
   protected-mode no  //关闭保护模式
   daemonize yes     //守护线程 ，后台运行redis
   appendonly yes    //aof日志每一个操作都记录模式
   cluster-enabled yes//开启集群模式
   cluster-config-file nodes-7000.conf  //集群节点的配置
   cluster-node-timeout 15000 //节点请求超时时间
   dir /usr/local/redis-cluster/7000/（指定数据文件存放位置，必须要指定不同的目录位置，不然会丢失数据）
   ```

   ```
   cp /usr/local/redis-cluster/7000/redis.conf /usr/local/redis-cluster/7001
   cp /usr/local/redis-cluster/7000/redis.conf /usr/local/redis-cluster/7002
   cp /usr/local/redis-cluster/7000/redis.conf /usr/local/redis-cluster/7003
   cp /usr/local/redis-cluster/7000/redis.conf /usr/local/redis-cluster/7004
   cp /usr/local/redis-cluster/7000/redis.conf /usr/local/redis-cluster/7005
   ```

6. 启动节点

   ```
   cd /usr/local/redis-5.0.5/src
   redis-server /usr/local/redis-cluster/7000/redis.conf
   redis-server /usr/local/redis-cluster/7001/redis.conf
   redis-server /usr/local/redis-cluster/7002/redis.conf
   redis-server /usr/local/redis-cluster/7003/redis.conf
   redis-server /usr/local/redis-cluster/7004/redis.conf
   redis-server /usr/local/redis-cluster/7005/redis.conf
   ```

   验证是否成功

   ```
   ps -ef | grep redis
   //退出redis服务
   pkill redis-server
   kill 进程号
   src/redis-cli shutdown
   ```

7. 防火墙开启 

   如果没有安装Firewall命令：

   ```
   yum install firewalld firewalld-config
   ```

   批量开启防火墙

   ```
   firewall-cmd --zone=public --add-port=7000-7005/udp --permanent
   firewall-cmd --zone=public --add-port=7000-7005/tcp --permanent
   ```

   开启集群总线端口 （节点端口+10000）

   ```
   firewall-cmd --zone=public --add-port=17000-17005/udp --permanent
   firewall-cmd --zone=public --add-port=17000-17005/tcp --permanent
   ```

   重启防火墙

   ```
   firewall-cmd --reload
   ```

8. 创建集群，--cluster-replicas 1指定从库数量1

   ```
   cd /usr/local/redis-5.0.5/src
   
   //有密码
   redis-cli -a 123123 --cluster create 192.168.10.122:7000 192.168.10.122:7001 192.168.10.122:7002 192.168.10.122:7003 192.168.10.122:7004 192.168.10.122:7005 --cluster-replicas 1
   //无密码
   redis-cli --cluster create 192.168.10.122:7000 192.168.10.122:7001 192.168.10.122:7002 192.168.10.122:7003 192.168.10.122:7004 192.168.10.122:7005 --cluster-replicas 1
   ```

   

9. 集群验证

   cluster info（查看集群信息）、cluster nodes（查看节点列表）

   ```
   //带密码登录
   /usr/local/redis-5.0.5/src/redis-cli -a 密码 -c -h 192.168.10.122 -p 7000
   
   //不带密码登录
   /usr/local/redis-5.0.5/src/redis-cli -c -h 192.168.84.35 -p 8001
   ```

10. 参考链接

    https://blog.csdn.net/weixin_30407099/article/details/102271026

    https://blog.csdn.net/leilei1366615/article/details/104017906?utm_medium=distribute.pc_relevant_right.none-task-blog-BlogCommendFromMachineLearnPai2-4.nonecase&depth_1-utm_source=distribute.pc_relevant_right.none-task-blog-BlogCommendFromMachineLearnPai2-4.nonecase
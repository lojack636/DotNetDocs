### 一、配置yum源

1. 下载mysql源安装包

   下载地址：https://dev.mysql.com/downloads/repo/yum/

   ![image-20200508135102762](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508135107.png)

   ![image-20200508135310667](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508135312.png)

   复制下载链接：https://dev.mysql.com/get/mysql80-community-release-el7-3.noarch.rpm

   进入执行目录，这里的目录是：/home

   执行下载命令：

   ```
   wget https://dev.mysql.com/get/mysql80-community-release-el7-3.noarch.rpm
   ```

2. 安装mysql源

   ```
   yum localinstall mysql80-community-release-el7-3.noarch.rpm
   ```

3. 检查是否安装成功

   ```
   yum repolist enabled | grep "mysql.*-community.*"
   ```

   ![image-20200508140717759](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508140719.png)

4. 安装mysql

   ```
   yum install mysql-community-server
   ```

### 二、启动mysql服务

1. 启动

   ```
   systemctl start mysqld
   或者
   service mysqld start
   ```

2. 查看启动状态

   ```
   systemctl status mysqld
   或
   service mysqld status
   ```

   ![image-20200508140646205](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508140648.png)

3. 设置开机启动

   ```
   systemctl enable mysqld
   systemctl daemon-reload
   ```

### 三、配置以及部分命令

1. 修改登录密码

   mysql安装完成后，会在/var/log/mysqld.log文件中给root默认生成一个密码。通过下面方式找到root默认密码，然后登录mysql进行修改

   ```
   grep 'temporary password' /var/log/mysqld.log
   ```

   ![image-20200508141154560](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508141156.png)

   本地登录mysql

   ```
   mysql -u root -p
   ```

   输入上面查询出来的密码，回撤进入

   修改密码（密码必须包含大小写字母、数字和特殊符号，并且长度不能少于8位）：

   ```
   ALTER USER 'root'@'localhost' IDENTIFIED BY 'TestBicon@123';
   或
   set password for 'root'@'localhost'=password('TestBicon@123');
   ```

2. 添加远程登录用户

   ```
   use mysql;
   select host, user from user;
   ```

   ```
   GRANT ALL ON *.* TO 'root'@'%';
   或
   update user set host='%' where user ='root';
   ```

   ```
   flush privileges;//命令刷新
   ```

3. Navicat、SQLyog连接时出错

   登录客户端，执行下面的命令：

   ```
   ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'password';
   ```

   如果报错：ERROR 1396 (HY000): Operation ALTER USER failed for 'root'@'localhost'则使用下面命令：

   ```
   ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'password';
   ```

4. 修改默认编码格式

   mysql8.0默认编码方式为utf8mb4，因此使用时不需要修改，可使用如下命令查看：

   ```
   SHOW VARIABLES WHERE Variable_name LIKE 'character_set_%' OR Variable_name LIKE 'collation%';
   ```

5. 查找mysql配置文件my.cnf

   ```
   mysql --help|grep 'my.cnf'
   ```

   ![image-20200508142341330](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508142343.png)

### 四、卸载mysql

1. 卸载软件

   ```
   yum remove mysql-community-server
   ```

   完成后使用rpm -qa|grep mysql命令查看，如果有查询结果，则使用yum remove 名称清理掉。如图：

   ![image-20200508142524967](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508142526.png)

   再使用命令rpm -qa | grep -i mysql查看，如果有结果使用rpm -e 名称卸载。如下：

   ![image-20200508142601077](https://gitee.com/50/PictureWarehouses/raw/master/img/20200508142602.png)

2. 删除文件

   ```
   rm -rf /var/lib/mysql
   rm /etc/my.cnf
   rm -rf /usr/share/mysql-8.0
   ```

   如果需要重新安装，在安装完成启动之前可以先对mysql目录赋予权限防止异常发生：

   ```
   chmod -R 777 /var/lib/mysql
   ```

   ### 五、附加

   1. systemctl命令

      ```
      systemctl is-enabled iptables.service
      systemctl is-enabled servicename.service #查询服务是否开机启动
      systemctl enable *.service #开机运行服务
      systemctl disable *.service #取消开机运行
      systemctl start *.service #启动服务
      systemctl stop *.service #停止服务
      systemctl restart *.service #重启服务
      systemctl reload *.service #重新加载服务配置文件
      systemctl status *.service #查询服务运行状态
      systemctl --failed #显示启动失败的服务
      ```

      


### Linux .Net Core Nginx部署

#### 1.环境安装
- .Net Core环境安装
 ```
1.注册Microsoft密钥 
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
2.SDK安装
sudo yum install dotnet-sdk-3.1
```
参考：https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-package-manager-centos7  
- Zip上传、解压插件
```
yum -y install unzip
yum install lrzsz
```
命令参考：https://blog.csdn.net/bettersun00/article/details/84018953 

#### 2.Nginx安装
- 安装依赖
```
yum -y install gcc zlib zlib-devel pcre-devel openssl openssl-devel
```
- 执行操作
```
1.  cd /usr/local
2.  mkdir nginx
3.  cd nginx
4.  wget http://nginx.org/download/nginx-1.15.1.tar.gz
5.  tar -xvf nginx-1.15.1.tar.gz
6.  cd nginx-1.15.1
7.  ./configure
8.  make && make install
```
- 修改配置
```
vi /usr/local/nginx/conf/nginx.conf
```
配置内容：  
```
server {
    listen 801;
    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```
- Nginx命令
```
#启动
/usr/local/nginx/sbin/nginx
#重启
/usr/local/nginx/sbin/nginx -s reload
#关闭进程
/usr/local/nginx/sbin/nginx -s stop
#平滑关闭
/usr/local/nginx/sbin/nginx -s quit
#查看安装状态
/usr/local/nginx/sbin/nginx -t
/usr/local/nginx/sbin/nginx -V
```

#### 3.程序部署     
- **程序运行**      
1、mkdir webappdev       
2、cd webappdev     
3、rz 上传压缩文件，这里我是publish.zip     
4、unzip publish.zip       
5、cd publish   
6、dotnet WebApp.dll    

- **注册开机启动服务**  
1、 cd /etc/systemd/system  
2.、touch [服务名称].service，配置内容如下：
```
[Unit]
Description=dotnet webappdev service

[Service]
WorkingDirectory=/root/webappdev/publish
ExecStart=/usr/bin/dotnet /root/webappdev/publish/WebApp.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=publish
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```
详解：  
参数 | 注解
---|---
Description | 服务描述  
WorkingDirectory | 服务工作的目录
ExecStart | 启动时执行的命令
Restart | 出错了是否重启(建议 always) 
RestartSec | 重启的时间
User | 用户
WantedBy | 该服务所在的连接地址

- **常用命令**
```
1.设置服务自动启动：systemctl enable [服务名称].service  
1.设置服务不自动启动：systemctl disable [服务名称].service
2.启动服务 systemctl  start  [服务名称].service
3.服务状态 systemctl  status [服务名称].service
4.重启服务 systemctl  restart [服务名称].service  
5.显示所有服务：systemctl list-units --all --type=service 
6.显示所有已启动服务：systemctl list-units --type=service
6.查看某一个服务 ps -elf | grep 服务名称
```
#### 4.附加
1.vm常用命令    
默认vim打开后是不能录入的，需要按键才能操作，具体如下： 
开启编辑：按“i”或者“Insert”键   
退出编辑：“Esc”键   
退出vim：“:q”   
保存vim：“:w”   
保存退出vim：“:wq”  
不保存退出vim：“:q!”  
#### 5.参考链接
https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?tabs=aspnetcore2x&view=aspnetcore-3.1  
https://www.cnblogs.com/dotnetlibao/p/11770027.html     
https://cloud.tencent.com/developer/article/1503591     
https://blog.csdn.net/zt102545/article/details/88578864  
https://www.jianshu.com/p/2ca8a2582701  




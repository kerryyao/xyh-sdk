# xyh_sdk
校园号sdk for .net core

开放平台及文档说明见: http://open.xiaoyuanhao.com

如对校园号开放接入有意向的同仁请在开放平台上申请开发权力，如有.net core层面的技术问题欢迎加入QQ群[295749240]进行交流学习！


校园号sdk使用说明

 1. 配置：
 
com.nbugs.xyh.appConfig.client_id = "client_id"

com.nbugs.xyh.appConfig.client_secret = "client_secret";

com.nbugs.xyh.appConfig.debug = false;

 1. 开放平台SDK接口使用说明

开发者只需要访问com.nbugs.xyh.open.XyhApiHelper类就可以获取所有接口的访问入口，该类以static方式提供了若干接口实例，开发者可直接调用。

1. 校园号新建用户中心，其中部分结构发生变动，增加相应的接口类XiaoYuanHaoHelper.cs
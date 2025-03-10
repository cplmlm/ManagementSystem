基于.Ne8框架搭建的一个通用的项目后台管理基础功能项目，包含登录、角色管理、权限管理、字典管理等。
# 项目介绍
项目主要整合了如下一些常用的技术点
- API 的授权认证采用 JWT 认证方式
- 全局异常记录实现，中间件和过滤器都有实现
- Redis分布式缓存实现
- 接入国产数据库ORM组件SqlSugar，封装数据库操作
- 使用Autofac
- 支持国密SM2、MD5、SM4
- 使用 Automapper 处理对象映射
- 使用 Swagger 做api文档
# 项目目录
- Common——公共文件
- Extensions——第三方组件
- IServices——业务逻辑接口
- Services——业务逻辑实现
- Repository——仓储类
- Model——实体类
- WebAPI——接口
# 项目功能
- 系统配置
- 行政区划
- 单位管理
- 数据字典
- 用户管理
- 角色管理
- 接口管理
- 分配权限
- 菜单管理
# 前端代码
- 项目前端代码：https://github.com/cplmlm/management-system-admin

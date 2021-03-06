##网易云音乐歌词下载器

网易音乐是目前最好用的音乐播放器，很多朋友喜欢从上面下载免费的音乐，但是可惜没法下载到对应的歌词。

这个小工具就是为你解决这个小问题的。

1、首先在网易云音乐网页版上搜到歌曲，复制地址栏里歌曲的ID；

![这里写图片描述](http://img.blog.csdn.net/20150913125937436)

2、把ID复制进小工具，即可获取歌词，就这么easy。

![这里写图片描述](http://img.blog.csdn.net/20150913125900915)

## 更新截图

### 163lyric
![Main Form](https://raw.githubusercontent.com/netcharm/163lyric/master/163lyric/Snapshots/sp_main.png)

![Search Result Form](https://raw.githubusercontent.com/netcharm/163lyric/master/163lyric/Snapshots/sp_searchresult.png)

### 163music

![Main Form](https://raw.githubusercontent.com/netcharm/163lyric/master/163music/snapshots/snap_main.png)

![Console Form](https://raw.githubusercontent.com/netcharm/163lyric/master/163music/snapshots/snap_console.png)

## 下载

1. [Bitbucket](https://bitbucket.org/netcharm/163lyric/downloads/)
1. [Github]

## 更新历史 By NetCharm

2017.04
-------
1. 163lyric 增加给定链接后批量下载专辑歌曲的歌词
1. 增加163music工具, 方便转换[greasemonkey脚本](https://raw.githubusercontent.com/netcharm/greasemonkey-code/master/music.163.com_cover.user.js)下载的专辑信息Markdown文
   件为HTML5格式的文件, 并自动修复一些简单的本地歌曲/专辑封面文件名问题.
   生成的HTML5文件, 支持显示专辑封面, 播放本地歌曲/同路径下的视频(*.mp4, *.ogg,*.webm)
   
   > 备注1: 支持点击Convert打开对话框选择markdown文件, 也支持直接拖拽markdown文件到窗口
   
   > 备注2: 此工具需要系统安装有[Pandoc](http://pandoc.org/), 请自行下载安装.
   
   > 备注3: 此工具需.net framework 4.0 以上。

2016.10
-------
1. 增加搜索结果窗口的搜索结果列表右键菜单
    1. 专辑封面/歌手照片 弹窗预览
    2. 选择拷贝歌曲相关信息到剪贴板, 如标题/歌手/专辑/出版商/封面地址/歌手照片
   
2016.04
-------
1. 支持多语言歌词(源语言歌词以及翻译的歌词)获取功能.
2. 支持输入歌曲的标题/演唱/专辑等信息查询, 最多显示前100个结果可供选择(受限于API, 可能只会显示和报告的结果总数不一致的查询结果),
   未实现翻页查询全部结果的功能(本人实际验证查询结果总数多于50+/100+的内容, 100以后的数据基本都是重复或者无实际参考价值的).

## 源代码：
* 原始: https://github.com/ituff/163lyric
* 修改版: 
    1. https://github.com/netcharm/163lyric
    2. https://bitbucket.org/netcharm/163lyric

```
注意：修改版运行平台为 .net framework 3.5 以上。
```


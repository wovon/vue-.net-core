<template>
  <!-- <article-detail :is-edit='true'></article-detail> -->
    <div class="createPost-container">
    <el-form class="form-container" :model="postForm" :rules="rules" ref="postForm">

      <sticky :className="'sub-navbar '+postForm.status">
        <template v-if="fetchSuccess">

          <router-link style="margin-right:15px;" v-show='isEdit' :to="{ path:'create-form'}">
            <el-button type="info">创建form</el-button>
          </router-link>

          <el-dropdown trigger="click">
            <el-button plain>{{!postForm.comment_disabled?'评论已打开':'评论已关闭'}}
              <i class="el-icon-caret-bottom el-icon--right"></i>
            </el-button>
            <el-dropdown-menu class="no-padding" slot="dropdown">
              <el-dropdown-item>
                <el-radio-group style="padding: 10px;" v-model="postForm.comment_disabled">
                  <el-radio :label="true">关闭评论</el-radio>
                  <el-radio :label="false">打开评论</el-radio>
                </el-radio-group>
              </el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>

          <el-dropdown trigger="click">
            <el-button plain>平台
              <i class="el-icon-caret-bottom el-icon--right"></i>
            </el-button>
            <el-dropdown-menu class="no-border" slot="dropdown">
              <el-checkbox-group v-model="postForm.platforms" style="padding: 5px 15px;">
                <el-checkbox v-for="item in platformsOptions" :label="item.key" :key="item.key">
                  {{item.name}}
                </el-checkbox>
              </el-checkbox-group>
            </el-dropdown-menu>
          </el-dropdown>

          <el-dropdown trigger="click">
            <el-button plain>
              外链
              <i class="el-icon-caret-bottom el-icon--right"></i>
            </el-button>
            <el-dropdown-menu class="no-padding no-border" style="width:300px" slot="dropdown">
              <el-form-item label-width="0px" style="margin-bottom: 0px" prop="source_uri">
                <el-input placeholder="请输入内容" v-model="postForm.source_uri">
                  <template slot="prepend">填写url</template>
                </el-input>
              </el-form-item>
            </el-dropdown-menu>
          </el-dropdown>

          <el-button v-loading="loading" style="margin-left: 10px;" type="success" @click="submitForm()">发布
          </el-button>
          <el-button v-loading="loading" type="warning" @click="draftForm">草稿</el-button>

        </template>
        <template v-else>
          <el-tag>发送异常错误,刷新页面,或者联系程序员</el-tag>
        </template>

      </sticky>

      <div class="createPost-main-container">

        <!-- 开始 -->
        <el-row>
          <el-col :span="23">
            <div class="postInfo-container">
              <el-row>
                <el-col :span="8">
                  <el-form-item label-width="65px" label="优先级:" content="" class="postInfo-container-item">
                    <multiselect :options="userLIstOptions" @search-change="getRemoteUserList" placeholder="重要，不重要" selectLabel="选择"
                      deselectLabel="删除" track-by="key" :internalSearch="false" label="key">
                      <span slot='noResult'>无结果</span>
                    </multiselect>
                  </el-form-item>
                </el-col>

                <el-col :span="8">
                  <el-tooltip class="item" effect="dark" :disabled="true" placement="top">
                    <el-form-item label-width="80px" label="类别:" class="postInfo-container-item">
                      <multiselect  style='min-width:223px;' :options="userLIstOptions" @search-change="getRemoteUserList" placeholder="PC端，手机端，都有，未知" selectLabel="选择"
                        deselectLabel="删除" track-by="key" :internalSearch="false" label="key">
                        <span slot='noResult'>无结果</span>
                      </multiselect>
                    </el-form-item>
                  </el-tooltip>
                </el-col>

                <el-col :span="8">
                  <el-form-item label-width="80px" label="添加时间:" class="postInfo-container-item">
                    <el-date-picker v-model="postForm.display_time" type="datetime" format="yyyy-MM-dd HH:mm:ss" placeholder="选择日期时间">
                    </el-date-picker>
                  </el-form-item>
                </el-col>

                <!-- <el-col :span="6">
                  <el-form-item label-width="80px" label="来源:" class="postInfo-container-item">
                    <multiselect v-model="postForm.author" :options="userLIstOptions" @search-change="getRemoteUserList" placeholder="搜索" selectLabel="选择"
                      deselectLabel="删除" track-by="key" :internalSearch="false" label="key">
                      <span slot='noResult'>无结果</span>
                    </multiselect>
                  </el-form-item>
                </el-col>
                <el-col :span="2">
                  <el-form-item label-width="0px" label="" class="postInfo-container-item">
                    <el-input placeholder="姓名" style='width:100px;' v-model="postForm.source_name"></el-input>
                  </el-form-item>
                </el-col> -->
              </el-row>
            </div>

          </el-col>
        </el-row>


        <el-row>
          <el-col :span="23">
            <div class="postInfo-container">
              <el-row>
                <el-col :span="8">
                  <el-form-item label-width="65px" label="项目:" class="postInfo-container-item">
                    <multiselect  :options="userLIstOptions" @search-change="getRemoteUserList" placeholder="所有项目下拉" selectLabel="选择"
                      deselectLabel="删除" track-by="key" :internalSearch="false" label="key">
                      <span slot='noResult'>无结果</span>
                    </multiselect>
                  </el-form-item>
                </el-col>

                <el-col :span="8">
                  <el-tooltip class="item" effect="dark" :disabled="true" placement="top">
                    <el-form-item label-width="80px" label="流程:" class="postInfo-container-item">
                      <multiselect  :options="userLIstOptions" @search-change="getRemoteUserList" placeholder="对应项目的流程下拉" selectLabel="选择"
                        deselectLabel="删除" track-by="key" :internalSearch="false" label="key">
                        <span slot='noResult'>无结果</span>
                      </multiselect>
                    </el-form-item>
                  </el-tooltip>
                </el-col>

                <el-col :span="8">
                  <el-form-item label-width="80px" label="上个问题:" class="postInfo-container-item">
                    <multiselect  :options="userLIstOptions" @search-change="getRemoteUserList" placeholder="标题筛选" selectLabel="选择"
                        deselectLabel="删除" track-by="key" :internalSearch="false" label="key">
                        <span slot='noResult'>无结果</span>
                      </multiselect>
                  </el-form-item>
                </el-col>
              </el-row>
            </div>

          </el-col>
        </el-row>

        <!-- 第二行 开始 -->
        <el-row>
          <el-col :span="23">
            <div class="postInfo-container">
              <el-row>
                <el-col :span="8">
                  <el-form-item label-width="65px" label="来源:" class="postInfo-container-item">
                    <el-input placeholder="" style='min-width:223px;' v-model="postForm.source_name">
                    </el-input>
                  </el-form-item>
                </el-col>

                <el-col :span="8">
                  <el-tooltip class="item" effect="dark" :disabled="true" placement="top">
                    <el-form-item label-width="80px" label="反馈给:" class="postInfo-container-item">
                      <multiselect v-model="postForm.author" :options="userLIstOptions" @search-change="getRemoteUserList" placeholder="反馈给谁" selectLabel="选择"
                        deselectLabel="删除" track-by="key" :internalSearch="false" label="key">
                        <span slot='noResult'>无结果</span>
                      </multiselect>
                    </el-form-item>
                  </el-tooltip>
                </el-col>

                <el-col :span="6">
                  <el-form-item label-width="80px" label="反馈时间:" class="postInfo-container-item">
                    <el-date-picker v-model="postForm.display_time" type="datetime" format="yyyy-MM-dd HH:mm:ss" placeholder="选择日期时间">
                    </el-date-picker>
                  </el-form-item>
                </el-col>
              </el-row>
            </div>
          </el-col>
        </el-row>
        <!-- 第二行 结束 -->

        <!-- 第三行 开始 -->
        <el-row>
          <el-col :span="24">
            <el-form-item style="margin-bottom: 40px;" prop="title">
              <MDinput name="name" v-model="postForm.title" required :maxlength="500">
                标题
              </MDinput>
              <span v-show="postForm.title.length>=500" class='title-prompt'>标题不能超过500字</span>
            </el-form-item>
          </el-col>
        </el-row>
        <!-- 第三行 结束 -->
        <!-- 编辑器 -->
        <div class="editor-container">
          <tinymce :height=400 ref="editor" v-model="postForm.content"></tinymce>
        </div>
        <!-- 文件上传 -->
        <div style="margin-bottom: 20px;">
          <Upload v-model="postForm.image_uri"></Upload>
        </div>
      </div>
    </el-form>

  </div>
</template>

<script>
//   import '@riophae/vue-treeselect/dist/vue-treeselect.css'
//   import {deptList} from "@/api/system/departments"
//   import Tinymce from '@/components/Tinymce'
//   import Upload from '@/components/Upload/singleImage3'
//   import MDinput from '@/components/MDinput'
//   import Multiselect from 'vue-multiselect'// 使用的一个多选框组件，element-ui的select不能满足所有需求
//   import 'vue-multiselect/dist/vue-multiselect.min.css'// 多选框组件css
//   import Sticky from '@/components/Sticky' // 粘性header组件
//   import { validateURL } from '@/utils/validate'
//   import { fetchArticle } from '@/api/article'
//   import { userSearch } from '@/api/remoteSearch'
// import ArticleDetail from './components/ArticleDetail'

//   export default {
//     // 注册组件
//     components: { ArticleDetail,Tinymce, MDinput, Upload, Multiselect, Sticky },
//     created(){
//       this.getDeptList();
//     },
//     methods: {
//       getDeptList(){
//         deptList().then(response=>{
//           //console.log(response.data.data)
//           this.treeData = response.data.data;
//         }).catch((e)=>{
//           console.log(e)
//         })
//       }
//     },

//     data() {
//       return {
//         defaultForm :{
//           status: 'draft',
//           title: '', // 文章题目
//           content: '', // 文章内容
//           content_short: '', // 文章摘要
//           source_uri: '', // 文章外链
//           image_uri: '', // 文章图片
//           source_name: '', // 文章外部作者
//           display_time: undefined, // 前台展示时间
//           id: undefined,
//           platforms: ['a-platform'],
//           comment_disabled: false
//         },
//         treeData:[],
//         // define default value
//         value: null,
//         // define options
//         options: [ {
//           id: 'a',
//           label: 'a',
//           children: [ {
//             id: 'aa',
//             label: 'aa',
//           }, {
//             id: 'ab',
//             label: 'ab',
//           } ],
//         }, {
//           id: 'b',
//           label: 'b',
//         }, {
//           id: 'c',
//           label: 'c',
//         } ],
//       }
//     },
//   }

import Tinymce from '@/components/Tinymce'
import Upload from '@/components/Upload/singleImage3'
import MDinput from '@/components/MDinput'
import Multiselect from 'vue-multiselect'// 使用的一个多选框组件，element-ui的select不能满足所有需求
import 'vue-multiselect/dist/vue-multiselect.min.css'// 多选框组件css
import Sticky from '@/components/Sticky' // 粘性header组件
import { validateURL } from '@/utils/validate'
import { fetchArticle } from '@/api/article'
import { userSearch } from '@/api/remoteSearch'
//路由

// const defaultForm = {
//   status: 'draft',
//   title: '', // 文章题目
//   content: '', // 文章内容
//   content_short: '', // 文章摘要
//   source_uri: '', // 文章外链
//   image_uri: '', // 文章图片
//   source_name: '', // 文章外部作者
//   display_time: undefined, // 前台展示时间
//   id: undefined,
//   platforms: ['a-platform'],
//   comment_disabled: false
// }


const defaultForm={
  Projects:'',//项目
  WorkFlows:'',//流程
  Title:'',//问题标题
  Content:'',//内容ID
  ParentId:'',//对应上一个问题ID
  FeedbackTime:'',//反馈时间
  FeedbackUserId:'',//反馈给用户ID
  FeedbackUserName:'',//反馈给谁
  Priority:'',//..优先级
  Modifiers:'',//指定修改人
  Histories:'',//修改人记录
  SelectOptions:'',//选项列表（这里对应修改状态）
  UserId:'',//用户ID
  UserName:'',//用户名
  DepartmentId:'',//部门ID
  DepartmentName:'',//部门名称
  Type:'',//类别 （手机端  PC端）
  Origin:'',//来源
  Sort:'',//排序
  CreateTime:'',//添加时间
  UpdateTime:'',//修改时间
  IsDel:false//是否删除
}


export default {
  name: 'editData',
  components: { Tinymce, MDinput, Upload, Multiselect, Sticky },
  // props: {
  //   isEdit: {
  //     type: Boolean,
  //     default: false
  //   }
  // },
  data() {
    const validateRequire = (rule, value, callback) => {
      if (value === '') {
        this.$message({
          message: rule.field + '为必传项',
          type: 'error'
        })
        callback(null)
      } else {
        callback()
      }
    }
    const validateSourceUri = (rule, value, callback) => {
      if (value) {
        if (validateURL(value)) {
          callback()
        } else {
          this.$message({
            message: '外链url填写不正确',
            type: 'error'
          })
          callback(null)
        }
      } else {
        callback()
      }
    }
    return {
      isEdit:true,
      postForm: Object.assign({}, defaultForm),
      fetchSuccess: true,
      loading: false,
      fa:false,
      userLIstOptions: [],
      platformsOptions: [
        { key: 'a-platform', name: 'a-platform' },
        { key: 'b-platform', name: 'b-platform' },
        { key: 'c-platform', name: 'c-platform' }
      ],
      rules: {
        image_uri: [{ validator: validateRequire }],
        title: [{ validator: validateRequire }],
        content: [{ validator: validateRequire }],
        source_uri: [{ validator: validateSourceUri, trigger: 'blur' }]
      }
    }
  },
  computed: {
    contentShortLength() {
      return this.postForm.content_short.length
    }
  },
  created() {
    //if (this.isEdit) {
      this.fetchData()
    // } else {
    //   this.postForm = Object.assign({}, defaultForm)
    // }
  },
  methods: {
    fetchData() {
      fetchArticle().then(response => {
        //console.log(response.data)
        this.postForm = response.data
      }).catch(err => {
        this.fetchSuccess = false
        console.log(err)
      })
    },
    //发布按钮事件
    submitForm() {
      this.postForm.display_time = parseInt(this.display_time / 1000)
      console.log(this.postForm)
      this.$refs.postForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$notify({
            title: '成功',
            message: '发布文章成功',
            type: 'success',
            duration: 2000
          })
          this.postForm.status = 'published'
          this.loading = false
          this.$router.push({path: '/system/users',query:{id: 'abc'}})
          // let view = {name:"测试",path:"",title:""};
          // this.$store.dispatch('delVisitedViews', view).then((views) => {
          //   console.log('tag', views)
          //   if(view.path === this.$route.path || view.name === this.$route.name)
          //   {
          //     const latestView = views.slice(-1)[0]
          //     if (latestView) {
          //       this.$router.push(latestView.path)
          //     } else {
          //       this.$router.push('/')
          //     }
          //   }
          // })
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    draftForm() {
      if (this.postForm.content.length === 0 || this.postForm.title.length === 0) {
        this.$message({
          message: '请填写必要的标题和内容',
          type: 'warning'
        })
        return
      }
      this.$message({
        message: '保存成功',
        type: 'success',
        showClose: true,
        duration: 1000
      })
      this.postForm.status = 'draft'
    },
    getRemoteUserList(query) {
      userSearch(query).then(response => {
        if (!response.data.items) return
        console.log(response)
        this.userLIstOptions = response.data.items.map(v => ({
          key: v.name
        }))
      })
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  @import "src/styles/mixin.scss";
  .title-prompt{
    position: absolute;
    right: 0px;
    font-size: 12px;
    top:10px;
    color:#ff4949;
  }
  .createPost-container {
    position: relative;
    .createPost-main-container {
      padding: 40px 45px 20px 50px;
      .postInfo-container {
        position: relative;
        @include clearfix;
        margin-bottom: 10px;
        .postInfo-container-item {
          float: left;
        }
      }
      .editor-container {
        min-height: 500px;
        margin: 0 0 30px;
        .editor-upload-btn-container {
            text-align: right;
            margin-right: 10px;
            .editor-upload-btn {
                display: inline-block;
            }
        }
      }
    }
    .word-counter {
      width: 40px;
      position: absolute;
      right: -10px;
      top: 0px;
    }
  }
</style>


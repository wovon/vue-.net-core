<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" v-for="b in buttons" @click="handleElement(b.code)" :key="b.Id" :icon="b.Icon" type="primary">{{b.name}}</el-button>
    </div>
    <tree-table :data="treeData" ref="projectTable" :evalFunc="func" @func="getRowSelect"  :evalArgs="args" :expandAll="expandAll" border >
      <el-table-column label="项目级别">
        <template slot-scope="scope">
          <span>{{scope.row.cascadeId}}</span>
        </template>
      </el-table-column>
      <el-table-column label="项目说明">
        <template slot-scope="scope">
          <span>{{scope.row.explain}}</span>
        </template>
      </el-table-column>
      <el-table-column label="对应流程">
        <template slot-scope="scope">
          <span style="color:red;">{{scope.row.workflowNames}}</span>
        </template>
      </el-table-column>
      <el-table-column label="排序" width="60" min-width="60px" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.sort}}</span>
        </template>
      </el-table-column>
      <el-table-column label="添加时间">
        <template slot-scope="scope">
          <span>{{scope.row.createTime | parseTime('{y}-{m}-{d}')}}</span>
        </template>
      </el-table-column>
    </tree-table>

    <!-- 弹出层（添加，修改，查看） -->
    <el-dialog :title="dialogMap[dialogStatus]" :visible.sync="dialogFromVisible">
      <el-form :rules="rules" ref="projectForm" :model="temp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="项目名称" >
          <el-input v-model="temp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="上一级" >
          <!-- <treeselect :options="treeData" :multiple="multiple" v-model="temp.parentId" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" /> -->
          <treeselect
          :multiple="true"
          :options="treeData"
          :disable-branch-nodes="false"
          v-model="temp.parentId"
          :value="temp.parentId"
          :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200"
          search-nested
          placeholder=""
          style="width:450px;"
          />
        </el-form-item>
        <!-- <el-form-item label="设置对应子类的ID" >
          <treeselect :options="treeData" v-model="temp.useChildId" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item> -->
        <el-form-item label="排  序">
          <el-input-number v-model="temp.sort" :min="1" :max="10000" label=""></el-input-number>
        </el-form-item>
        <el-form-item label="项目说明">
          <el-input type="textarea" :autosize="{ minRows: 4, maxRows: 8}" placeholder="请输入内容" v-model="temp.explain" style="width:450px">
          </el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFromVisible=false" >取消</el-button>
        <el-button v-if="dialogStatus=='add'" @click="handleAdd" type="primary">添加</el-button>
        <el-button v-if="dialogStatus=='edit'" @click="handleEidt" type="primary">修改</el-button>
        <el-button v-if="dialogStatus=='see'" @click="dialogFromVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>
     <!-- 设置对应的流程 -->
    <el-dialog title="设置流程" :visible.sync="dialogSetWorkflowVisible">
      <el-form ref="projectForm" :model="temp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名称" >
          <el-input v-model="setProjectWorkflowtemp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="流程" >
          <treeselect :options="workflowData" v-model="setProjectWorkflowtemp.workflows" :value="setProjectWorkflowtemp.workflows" :multiple="true" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true"
          :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetWorkflowVisible=false" >取消</el-button>
        <el-button @click="handleSetWorkflow();dialogSetWorkflowVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>
    <!-- 删除提示 -->
    <el-dialog title="提示" :visible.sync="dialogConfirm" style="width:666px;margin:0 auto;">
      <span v-html="confirmData.content"></span>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogConfirm = false">取消</el-button>
        <el-button type="primary" @click="handleDelete();dialogConfirm = false">确定</el-button>
      </span>
    </el-dialog>
  </div>
</template>


<script>
import treeTable from '@/components/TreeTable'
import treeToArray from '@/views/example/table/treeTable/customEval'
import {projectList,projectAdd,projectEdit,projectDelete,setProjectWorkflow} from '@/api/system/projects'
import {workflowList,LoadByProjectIds} from "@/api/workflow/workflows"
import { parseTime } from "@/utils";

// 下拉树
import Treeselect from '@riophae/vue-treeselect'
import '@riophae/vue-treeselect/dist/vue-treeselect.css'

export default {
    name:'customProjectTable',
    components: { treeTable,Treeselect },
    data(){
        return{
            func: treeToArray,
            expandAll: true,
            treeData:[],
            buttons:[],
            workflowData:[],
            args: [null, null, ''],
            rowSelect: [],
            dialogSetWorkflowVisible:false,
            dialogConfirm:false,
            confirmData: {title:"",content:"确定要删除这条数据？"},
            dialogFromVisible:false,
            dialogStatus:"",
            dialogMap:{add:"添加",delete:"删除",edit:"修改",see:"查看",setworkflow:"设置流程"},
            temp:{
                id:'',
                name:'',               
                parentId:[],
                parentName:'',
                cascadeId:'',
                label:'',
                explain:'',
                createTime:new Date(),
                updateTime:new Date(),
                isAble:false,
                isDel:false,
                sort:1000
            },
            rules: {
                name: [{ required: true, message: '请填写名称', trigger: 'blur' }]
            },
            setProjectWorkflowtemp:{
              name:"",
              projects:[],
              workflows:[],
              data:[]
            }
        }
    },
    created(){
        this.getButtons()
        this.getProList()
        this.getWorkflowList()
    },
    methods: {
        getButtons() {
            this.buttons = [
                {id: 1,name: "添加",code: "add",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                {id: 2,name: "删除",code: "delete",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                {id: 3,name: "修改",code: "edit",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                {id: 4,name: "查看",code: "see",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                {id: 5,name: "设置流程",code: "setworkflow",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                // {id: 6,name: "全部展开",code: "expandall",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                // {id: 7,name: "全部收缩",code: "collapseall",icon: "el-icon-edit",explain: "", createTime: "",updateTime: "",sort: 1000}
              ];
            // let _Queery = {name:this.$route.name,component:this.$route.path.substr(1)}
            // buttonListByMenuName(_Queery).then(response => {
            //     this.buttons = response.data.data
            // })
        },
        getWorkflowList(){
          workflowList().then(response=>{
            //this.treeData = response.data.data;
            this.workflowData = response.data.data;
            console.log(this.workflowData)
          }).catch((e)=>{
              //console.log(e)
          })
        },
        //项目对应流程
        async getProjectWorkflowList(s){
          await LoadByProjectIds(s).then(response=>{
            var ids = []
            response.data.data.forEach(row=>{
              ids.push(row.id)
            })
            this.setProjectWorkflowtemp.workflows = ids
            this.dialogSetWorkflowVisible = true
          }).catch(e=>{
          })
        },
        clearProjectTemp(){
          this.setProjectWorkflowtemp = {
            name:"",
            projects:[],
            workflows:[],
            data:[]
          }
        },
        getProList(){
            projectList().then(response=>{
                this.treeData = response.data.data;
                console.log(this.treeData)
            }).catch((e)=>{
                console.log(e);
            })
        },
        message(row) {
            this.$message.info(row.event)
        },
        getRowSelect(rows){
            this.rowSelect = rows
        },
        handleElement(code){
            code = code.toLocaleLowerCase()
            this.dialogStatus = code;
            if(code==="expandall")
            {
                this.expandAll = true
            }
            else if(code==="collapseall")
            {
                this.expandAll = false
            }
            //添加
            else if(code==="add")
            {
                this.dialogFromVisible = true
            }
            //删除
            else if(code==="delete")
            {
                if(this.rowSelect.length==0)
                {
                this.$notify({title:"提示",message:"请选择要删除的数据",type:"info",duration:2000})
                }
                else if (this.rowSelect.length > 0) {
                  this.dialogConfirm=true
                  this.confirmData.content = '你确定要删除这'+this.rowSelect.length+'条数据？删除之后相关联的下级也将删除。<font color="red">如有共用下级情况，对应下级也将删除。请谨慎操作。</font>'
                }
            }
            // 修改
            else if(code==="edit" || code==="see")
            {

                if(this.rowSelect.length===0)
                {
                this.$notify({title: "提示",message: "请选择要"+this.dialogMap[this.dialogStatus]+"的数据",type: "info",duration: 2000})
                }
                else if(this.rowSelect.length>1)
                {
                this.$notify({title:"提示",message:"不支持多条数据"+this.dialogMap[this.dialogStatus],type:"info",duration:2000})
                this.$refs.projectTable.handleClearSelection()
                }
                else
                {
                this.temp = Object.assign({},this.rowSelect[0])
                delete this.temp.parent
                delete this.temp.children
                this.temp.parentId = this.temp.parentId!=undefined?this.temp.parentId.split(','):[];
                this.dialogFromVisible = true
                this.$nextTick(() => {
                this.$refs["projectForm"].clearValidate()
                });
                }
            }//设置流程
            else if(code=="setworkflow")
            {
              if(this.rowSelect.length===0)
              {
                this.$notify({title: "提示",message: "请选择要"+this.dialogMap[this.dialogStatus]+"的数据",type: "info",duration: 2000})
              }
              else
              {
                var ids = "";
                this.clearProjectTemp()
                this.rowSelect.forEach((row,index)=>{
                  this.setProjectWorkflowtemp.name += row.name+","
                  this.setProjectWorkflowtemp.projects.push(row.id)
                  ids+=row.id+",";
                })
                this.setProjectWorkflowtemp.name = this.setProjectWorkflowtemp.name.replace(/,$/gi,"")
                this.getProjectWorkflowList({projectIds:ids})
                //this.dialogSetRoleVisible = true
              }
            }
        },
        handleAdd(){
            const tempData = Object.assign({},this.temp)
            tempData.parentId = tempData.parentId!=undefined?tempData.parentId.join(','):'';
            this.$refs["projectForm"].validate(valid => {
                projectAdd(tempData).then(response=>{
                if(response.data.code===200)
                {
                    this.$notify({title: "成功",message: "添加成功！",type: "success",duration: 2000})
                    this.dialogFromVisible = false;
                    this.getProList();
                }
                else{
                    this.$notify({title: "失败",message: "添加失败！",type: "error",duration: 2000})
                }
                })
                .catch((e)=>{
                this.$notify({title: "失败",message: "添加失败！catch",type: "error",duration: 2000})
                })
            })
        },
        handleDelete(){
            let _ids={ ids: "" }
            this.rowSelect.forEach((row,index)=>{
                _ids.ids+=row.id+","
            })
            if(_ids.ids!="")
            {
                projectDelete(_ids).then(response=>{
                if(response.data.code===200)
                {
                    this.$notify({title: "成功",message: this.dialogMap[this.dialogStatus]+"成功！",type: "success",duration: 2000})
                    this.getProList();
                }
                else
                {
                    this.$notify({title: "失败",message: this.dialogMap[this.dialogStatus]+"失败！",type: "error",duration: 2000})
                }
                })
                .catch(e=>{
                this.$notify({title: "失败",message: this.dialogMap[this.dialogStatus]+"失败！catch",type: "error",duration: 2000})
                })
            }
          },
          handleEidt(){
            const tempData = Object.assign({},this.temp)
            tempData.parentId = tempData.parentId!=undefined?tempData.parentId.join(','):'';

            this.$refs["projectForm"].validate(valid => {
                projectEdit(tempData).then(response=>{
                if(response.data.code===200)
                {
                    this.dialogFromVisible = false
                    this.$notify({title:"成功",message:"修改成功",type:"success",duration:2000})
                    this.getProList();
                }
                else
                {
                    this.$notify({title:"失败",message:"修改失败",type:"error",duration:2000})
                }
                })
                .catch(e=>{
                this.$notify({title: "失败",message: "修改失败！catch",type: "error",duration: 2000})
                })
              })
            },
            handleSetWorkflow(){
              this.setProjectWorkflowtemp.projects.forEach((row,index)=>{
                this.setProjectWorkflowtemp.workflows.forEach((row2)=>{
                  this.setProjectWorkflowtemp.data.push({projectId:row,workflowId:row2})
                })
                if(this.setProjectWorkflowtemp.data.length==0)
                {
                  this.setProjectWorkflowtemp.data.push({projectId:"",workflowId:row})
                }
              })
              console.log(this.setProjectWorkflowtemp.data)
              setProjectWorkflow({projectWorkflows:this.setProjectWorkflowtemp.data}).then((response)=>{
                if(response.data.code===200)
                {
                  this.getProList();
                  this.dialogSetWorkflowVisible = false
                  this.$notify({title:"成功",message:"设置成功",type:"success",duration:2000})
                  this.$refs.projectTable.handleClearSelection()
                }
                else
                {
                  this.$notify({title:"失败",message:"设置失败",type:"error",duration:2000})
                }
              }).catch(e=>{
                    this.$notify({title: "失败",message: "设置失败！catch",type: "error",duration: 2000})
              })
            }
    }
}
</script>

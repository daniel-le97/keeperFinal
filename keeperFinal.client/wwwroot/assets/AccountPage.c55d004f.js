import{_ as y,c as n,a as e,t as b,n as d,F as V,r as C,b as v,d as i,A as t,M as k,P as x,K as w,V as P,u as j,o as l,e as N,p as S,f as A,g as h,h as I}from"./index.ec86ee40.js";const r={setup(){return{account:i(()=>t.account),keeps:i(()=>t.keeps),vaults:i(()=>t.filterVaults),length:i(()=>t.filterVaults.length<=4),publicNum:i(()=>t.public),privateNum:i(()=>t.private),imgC:i(()=>{var s;return`url(${(s=t.account)==null?void 0:s.coverImg})`}),size:i(()=>window.innerWidth<=768),editAccount(){t.modal=1,k.getOrCreateInstance("#accountForm").show()},togglePub(){if(t.public=!t.public,t.public==!0){t.private=!1,t.filterVaults=t.userVaults.filter(s=>s.isPrivate!=!0);return}t.filterVaults=t.userVaults},togglePrivate(){if(t.private=!t.private,t.private==!0){t.public=!1,t.filterVaults=t.userVaults.filter(s=>s.isPrivate==!0);return}t.filterVaults=t.userVaults}}},components:{ProfileDetail:x,KeepCard:w,VaultCard:P}},m=()=>{j(s=>({"5c423bb8":s.imgC}))},f=r.setup;r.setup=f?(s,c)=>(m(),f(s,c)):m;const B=r,u=s=>(S("data-v-4ddcc820"),s=s(),A(),s),z={class:"container"},F={class:"row justify-content-center"},D={class:"col-md-10"},K={class:"container-fluid"},M={class:"row justify-content-center"},E={class:"col-md-8"},L={class:"m-3"},O={class:"card-container"},T={class:"top-card rounded position-relative"},W=["src"],q=u(()=>e("div",{class:"bottom-card d-flex justify-content-center"},null,-1)),G={class:"row justify-content-evenly"},H={class:"col-12 text-center fs-1 mb-5"},J={class:"col-12 text-center"},Q={key:0,class:"d-flex justify-content-center gap-4 m-3"},R=h(" public "),U=u(()=>e("i",{class:"mdi mdi-play mdi-rotate-90"},null,-1)),X=[R,U],Y=h(" private "),Z=u(()=>e("i",{class:"mdi mdi-play mdi-rotate-90"},null,-1)),$=[Y,Z],tt={key:1},et={class:"col-12"},st={key:0,class:"row collapse show justify-content-evenly",id:"public"};function ot(s,c,ct,o,at,it){var _,p;const g=I("VaultCard");return l(),n("div",z,[e("div",F,[e("div",D,[e("div",K,[e("div",M,[e("div",E,[e("div",L,[e("div",O,[e("div",T,[e("i",{class:"mdi mdi-dots-horizontal fs-1 text-danger dots selectable rounded",onClick:c[0]||(c[0]=a=>o.editAccount()),title:"edit account"}),e("img",{src:(_=o.account)==null?void 0:_.picture,alt:"icon"},null,8,W)]),q])])])]),e("div",G,[e("div",H,b((p=o.account)==null?void 0:p.name),1),e("div",J,[o.vaults?(l(),n("div",Q,[e("div",{onClick:c[1]||(c[1]=a=>o.togglePub()),class:d(o.publicNum?"text-primary fw-bold":"")},X,2),e("div",{onClick:c[2]||(c[2]=a=>o.togglePrivate()),class:d(o.privateNum?"text-primary fw-bold":"")},$,2)])):(l(),n("div",tt," no vaults"))]),e("div",et,[o.vaults?(l(),n("div",st,[(l(!0),n(V,null,C(o.vaults,a=>(l(),n("div",{class:d(o.length?"col-md-6":"col-md-3"),key:a==null?void 0:a.id},[a?(l(),N(g,{key:0,vault:a},null,8,["vault"])):v("",!0)],2))),128))])):v("",!0)])])])])])])}const nt=y(B,[["render",ot],["__scopeId","data-v-4ddcc820"]]);export{nt as default};

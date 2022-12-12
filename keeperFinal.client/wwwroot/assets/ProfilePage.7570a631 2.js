import{q as u,A as l,s as K,v as b,x as S,y as j,_ as B,i as D,j as f,d as p,P as E,K as I,V as N,c as i,a as e,e as z,b as A,t as m,F,r as R,l as h,z as q,B as H,C as L,h as _,o as c,p as M,f as T,g as W}from"./index.ec86ee40.js";class G{async getProfile(o){const a=await u.get("api/profiles/"+o);l.activeProfile=new K(a.data)}async getProfileKeeps(o){const a=await u.get(`api/profiles/${o}/keeps`);l.keeps=a.data.map(t=>new b(t))}async getProfileVaults(o){const a=await u.get(`api/profiles/${o}/vaults`);console.log(a.data);let t=a.data.map(r=>new S(r));l.vaults=t,j.log(l.vaults)}}const v=new G;const J={setup(){const s=H(),o=L();D(()=>{a(),t(),r()});async function a(){try{await v.getProfile(s.params.id)}catch(n){o.push({name:"Home"}),f.error(n)}}async function t(){try{await v.getProfileKeeps(s.params.id)}catch(n){f.error(n)}}async function r(){try{await v.getProfileVaults(s.params.id)}catch(n){f.error(n)}}return{profile:p(()=>l.activeProfile),items:p(()=>l.keeps),vaults:p(()=>l.vaults),size:p(()=>window.innerWidth<=768)}},components:{ProfileDetail:E,KeepCard:I,VaultCard:N}},g=s=>(M("data-v-d2ff26c9"),s=s(),T(),s),O={class:"container"},Q={class:"row justify-content-center"},U={class:"col-md-10"},X={class:"container-fluid"},Y={class:"row justify-content-center"},Z={class:"col-md-8"},$={class:"row justify-content-center"},ee={class:"col-md-4 col-8 d-flex justify-content-between rounded-pill orange text-shadow"},te={key:0},se={key:1},ae=g(()=>e("span",{class:"bg-white one rounded"},null,-1)),oe={class:"row justify-content-evenly"},le=g(()=>e("div",{class:"col-12 text-center"},[e("h1",{class:"btn fs-3",title:"vault collapse button","data-bs-toggle":"collapse","data-bs-target":"#collapseExample","aria-expanded":"false","aria-controls":"collapseExample"},[W(" vaults "),e("i",{class:"mdi mdi-play mdi-rotate-90 fs-4"})])],-1)),ne={class:"row"},ce={class:"col-12"},ie=g(()=>e("div",{class:"text-center"},[e("h1",null,"keeps")],-1));function re(s,o,a,t,r,n){var w,y,P;const x=_("ProfileDetail"),V=_("VaultCard"),k=_("KeepCard"),C=_("masonry-wall");return c(),i("div",O,[e("div",Q,[e("div",U,[e("div",X,[e("div",Y,[e("div",Z,[t.profile?(c(),z(x,{key:0,profile:t.profile},null,8,["profile"])):A("",!0)])]),e("div",$,[e("div",ee,[t.vaults.length==1?(c(),i("span",te,m((w=t.vaults)==null?void 0:w.length)+" Vault",1)):(c(),i("span",se,m((y=t.vaults)==null?void 0:y.length)+" Vaults",1)),ae,e("span",null,m((P=t.items)==null?void 0:P.length)+" Keeps",1)])]),e("div",oe,[le,(c(!0),i(F,null,R(t.vaults,d=>(c(),i("div",{class:"col-6 col-md-3 height my-3 collapse show",id:"collapseExample",key:d.id},[h(V,{vault:d},null,8,["vault"])]))),128))]),e("div",ne,[e("div",ce,[ie,h(C,{items:t.items,"column-width":t.size?150:250,gap:16},{default:q(({item:d,index:de})=>[e("div",null,[h(k,{keep:d},null,8,["keep"])])]),_:1},8,["items","column-width"])])])])])])])}const _e=B(J,[["render",re],["__scopeId","data-v-d2ff26c9"]]);export{_e as default};
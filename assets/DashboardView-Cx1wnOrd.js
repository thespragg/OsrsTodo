import{s as w,f as z,a as S,b as _}from"./index-C4sCHeKQ.js";import{B as k,c as l,o as s,r as P,a as $,b as A,m as p,t as f,n as B,d as C,e as E,u as j,f as e,g,h as c,w as u,i as L,j as N}from"./index-BM4TVkqT.js";var O=`
    .p-avatar {
        display: inline-flex;
        align-items: center;
        justify-content: center;
        width: dt('avatar.width');
        height: dt('avatar.height');
        font-size: dt('avatar.font.size');
        background: dt('avatar.background');
        color: dt('avatar.color');
        border-radius: dt('avatar.border.radius');
    }

    .p-avatar-image {
        background: transparent;
    }

    .p-avatar-circle {
        border-radius: 50%;
    }

    .p-avatar-circle img {
        border-radius: 50%;
    }

    .p-avatar-icon {
        font-size: dt('avatar.icon.size');
        width: dt('avatar.icon.size');
        height: dt('avatar.icon.size');
    }

    .p-avatar img {
        width: 100%;
        height: 100%;
    }

    .p-avatar-lg {
        width: dt('avatar.lg.width');
        height: dt('avatar.lg.width');
        font-size: dt('avatar.lg.font.size');
    }

    .p-avatar-lg .p-avatar-icon {
        font-size: dt('avatar.lg.icon.size');
        width: dt('avatar.lg.icon.size');
        height: dt('avatar.lg.icon.size');
    }

    .p-avatar-xl {
        width: dt('avatar.xl.width');
        height: dt('avatar.xl.width');
        font-size: dt('avatar.xl.font.size');
    }

    .p-avatar-xl .p-avatar-icon {
        font-size: dt('avatar.xl.icon.size');
        width: dt('avatar.xl.icon.size');
        height: dt('avatar.xl.icon.size');
    }

    .p-avatar-group {
        display: flex;
        align-items: center;
    }

    .p-avatar-group .p-avatar + .p-avatar {
        margin-inline-start: dt('avatar.group.offset');
    }

    .p-avatar-group .p-avatar {
        border: 2px solid dt('avatar.group.border.color');
    }

    .p-avatar-group .p-avatar-lg + .p-avatar-lg {
        margin-inline-start: dt('avatar.lg.group.offset');
    }

    .p-avatar-group .p-avatar-xl + .p-avatar-xl {
        margin-inline-start: dt('avatar.xl.group.offset');
    }
`,V={root:function(t){var r=t.props;return["p-avatar p-component",{"p-avatar-image":r.image!=null,"p-avatar-circle":r.shape==="circle","p-avatar-lg":r.size==="large","p-avatar-xl":r.size==="xlarge"}]},label:"p-avatar-label",icon:"p-avatar-icon"},D=k.extend({name:"avatar",style:O,classes:V}),T={name:"BaseAvatar",extends:w,props:{label:{type:String,default:null},icon:{type:String,default:null},image:{type:String,default:null},size:{type:String,default:"normal"},shape:{type:String,default:"square"},ariaLabelledby:{type:String,default:null},ariaLabel:{type:String,default:null}},style:D,provide:function(){return{$pcAvatar:this,$parentInstance:this}}};function d(a){"@babel/helpers - typeof";return d=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},d(a)}function m(a,t,r){return(t=R(t))in a?Object.defineProperty(a,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):a[t]=r,a}function R(a){var t=U(a,"string");return d(t)=="symbol"?t:t+""}function U(a,t){if(d(a)!="object"||!a)return a;var r=a[Symbol.toPrimitive];if(r!==void 0){var o=r.call(a,t);if(d(o)!="object")return o;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(a)}var b={name:"Avatar",extends:T,inheritAttrs:!1,emits:["error"],methods:{onError:function(t){this.$emit("error",t)}},computed:{dataP:function(){return z(m(m({},this.shape,this.shape),this.size,this.size))}}},Y=["aria-labelledby","aria-label","data-p"],q=["data-p"],I=["data-p"],K=["src","alt","data-p"];function W(a,t,r,o,v,n){return s(),l("div",p({class:a.cx("root"),"aria-labelledby":a.ariaLabelledby,"aria-label":a.ariaLabel},a.ptmi("root"),{"data-p":n.dataP}),[P(a.$slots,"default",{},function(){return[a.label?(s(),l("span",p({key:0,class:a.cx("label")},a.ptm("label"),{"data-p":n.dataP}),f(a.label),17,q)):a.$slots.icon?(s(),$(C(a.$slots.icon),{key:1,class:B(a.cx("icon"))},null,8,["class"])):a.icon?(s(),l("span",p({key:2,class:[a.cx("icon"),a.icon]},a.ptm("icon"),{"data-p":n.dataP}),null,16,I)):a.image?(s(),l("img",p({key:3,src:a.image,alt:a.ariaLabel,onError:t[0]||(t[0]=function(){return n.onError&&n.onError.apply(n,arguments)})},a.ptm("image"),{"data-p":n.dataP}),null,16,K)):A("",!0)]})],16,Y)}b.render=W;const F={class:"min-h-screen bg-gray-50"},G={class:"bg-white shadow"},H={class:"max-w-7xl mx-auto px-4 sm:px-6 lg:px-8"},J={class:"flex justify-between items-center py-6"},M={class:"text-gray-600"},Q={class:"flex items-center space-x-4"},X={class:"max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8"},ta=E({__name:"DashboardView",setup(a){const{currentUser:t,signOut:r}=j(),o=L(),v=async()=>{await r(),o.push("/login")};return(n,i)=>{const h=b,y=S,x=_;return s(),l("div",F,[e("div",G,[e("div",H,[e("div",J,[e("div",null,[i[0]||(i[0]=e("h1",{class:"text-3xl font-bold text-gray-900"},"OSRS Todo",-1)),e("p",M,"Welcome back, "+f(g(t)?.username)+"!",1)]),e("div",Q,[c(h,{label:g(t)?.username?.charAt(0).toUpperCase(),class:"bg-blue-500 text-white"},null,8,["label"]),c(y,{onClick:v,severity:"secondary",outlined:""},{default:u(()=>i[1]||(i[1]=[e("i",{class:"pi pi-sign-out mr-2"},null,-1),N(" Sign Out ")])),_:1,__:[1]})])])])]),e("div",X,[c(x,null,{header:u(()=>i[2]||(i[2]=[e("div",{class:"p-6 border-b"},[e("h2",{class:"text-xl font-semibold"},"Dashboard")],-1)])),content:u(()=>i[3]||(i[3]=[e("div",{class:"text-center py-12"},[e("i",{class:"pi pi-check-circle text-6xl text-green-500 mb-4"}),e("h3",{class:"text-xl font-semibold mb-2"},"You're all set!"),e("p",{class:"text-gray-600"},"Your todo app is ready to go.")],-1)])),_:1})])])}}});export{ta as default};

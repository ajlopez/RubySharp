# RubySharp

Ruby interpreter written in C#.

Work in Progress

## Origin

It's a new implementation of previous work https://github.com/ajlopez/AjRuby

It's an interpreter, not a compiler. It keeps a tree of commands and expressions that are executed and evaluated.

## References

- [True, False And Nil Objects In Ruby](http://www.skorks.com/2009/09/true-false-and-nil-objects-in-ruby/)
- [Why should you avoid the then keyword in Ruby?](http://stackoverflow.com/questions/5659360/why-should-you-avoid-the-then-keyword-in-ruby)
- [Ruby Language Reference Manual](http://web.njit.edu/all_topics/Prog_Lang_Docs/html/ruby/index.html)
- [Ruby Syntax](http://web.njit.edu/all_topics/Prog_Lang_Docs/html/ruby/syntax.html)
- [Ruby Documentation](http://www.ruby-lang.org/en/documentation/)
- [The Ruby Language](http://www.rubycentral.com/pickaxe/language.html)
- [Define method for instance of class](http://stackoverflow.com/questions/3026943/define-method-for-instance-of-class)
- [Ruby: Dynamically Define Method](http://blog.jayfields.com/2008/02/ruby-dynamically-define-method.html)
- [Class and Instance Methods in Ruby](http://www.railstips.org/blog/archives/2009/05/11/class-and-instance-methods-in-ruby/)
- [Test for instance method existence?](http://www.ruby-forum.com/topic/142523)
- [Ruby Metaprogramming: Declaratively Adding Methods to a Class](http://www.vitarara.org/cms/ruby_metaprogamming_declaratively_adding_methods_to_a_class)
- [How do I build DSLs with yield and instance_eval?](http://rubylearning.com/blog/2010/11/30/how-do-i-build-dsls-with-yield-and-instance_eval/)
- [DSL in Ruby](http://4loc.wordpress.com/2009/05/29/dsl-in-ruby/)
- [Python's method decorators for Ruby](https://github.com/michaelfairley/method_decorators)
- [Ruby: Class Methods](http://blog.jayfields.com/2007/04/ruby-class-methods.html)
- [Class and Instance Methods in Ruby](http://www.railstips.org/blog/archives/2009/05/11/class-and-instance-methods-in-ruby/)
- [Creating your own attr_accessor in Ruby](http://mikeyhogarth.wordpress.com/2011/12/01/creating-your-own-attr_accessor-in-ruby/)
- [What is an accessor?](http://www.rubyist.net/~slagell/ruby/accessors.html)
- [Ways to define a global method in ruby](http://stackoverflow.com/questions/7188100/ways-to-define-a-global-method-in-ruby)
- [Ruby: why does puts call to_ary?](http://stackoverflow.com/questions/8960685/ruby-why-does-puts-call-to-ary)
- [How "puts" works in Ruby](http://www.caioromao.com/blog/how-puts-works-in-ruby/)
- [What are ancestors in Ruby and what is their use?](http://stackoverflow.com/questions/4989383/what-are-ancestors-in-ruby-and-what-is-their-use)
- [Ruby Metaprogramming](http://ruby-metaprogramming.rubylearning.com/)
- [Ruby Metaprogramming 2](http://ruby-metaprogramming.rubylearning.com/html/ruby_metaprogramming_2.html)
- [Extending your include knowledge of Ruby](http://macournoyer.wordpress.com/2007/07/06/extending-your-include-knowledge-of-ruby/)
- [Ruby's Eigenclasses Demystified](http://madebydna.com/all/code/2011/06/24/eigenclasses-demystified.html)
- [Reflection, ObjectSpace, and Distributed Ruby](http://www.rubycentral.org/pickaxe/ospace.html)
- [Understanding Ruby Metaprogramming](http://dfmonaco.github.io/understanding_ruby_metaprogramming/#/)
- [Ruby Metaprogramming: Declaratively Adding Methods to a Class](http://www.vitarara.org/cms/ruby_metaprogamming_declaratively_adding_methods_to_a_class)
- [Things that clear Ruby's method cache](https://charlie.bz/blog/things-that-clear-rubys-method-cache)
- [Parallelism is a Myth in Ruby](http://www.igvita.com/2008/11/13/concurrency-is-a-myth-in-ruby/)
- [Ruby Tips Part 2](http://globaldev.co.uk/2013/09/ruby-tips-part-2/)
- [Ruby Glossary](http://www.codecademy.com/glossary/ruby)
- [Ruby QuickRef](http://zenspider.com/Languages/Ruby/QuickRef.html)
- [Evaluating alternative Decorator implementations in Ruby](http://robots.thoughtbot.com/post/14825364877/evaluating-alternative-decorator-implementations-in)
- [Tidy views and beyond with Decorators](http://robots.thoughtbot.com/post/13641910701/tidy-views-and-beyond-with-decorators)
- [How Ruby method dispatch works](http://blog.jcoglan.com/2013/05/08/how-ruby-method-dispatch-works/)
- [MetaProgramming - Extending Ruby for Fun and Profit](http://www.infoq.com/presentations/metaprogramming-ruby)
- [Ruby Loops](http://www.tutorialspoint.com/ruby/ruby_loops.htm)
- [Ruby Methods](http://www.tutorialspoint.com/ruby/ruby_methods.htm)
- [Array](http://ruby-doc.org/core-2.0.0/Array.html)
- [Class Module &lt; Object](http://phrogz.net/programmingruby/ref_c_module.html) see module_function
- [Can I invoke an instance method on a Ruby module without including it?](http://stackoverflow.com/questions/322470/can-i-invoke-an-instance-method-on-a-ruby-module-without-including-it) see module_function
- [Ruby – Visibility of private and protected module methods when mixed into a class](http://www.treibstofff.de/2009/08/07/ruby-visibility-of-private-and-protected-module-methods-when-mixed-into-a-class/)
- [Use of Double-Colon ::](https://www.ruby-forum.com/topic/107527)
- [What does :: (double colon) mean in Ruby?](http://stackoverflow.com/questions/2276905/what-does-double-colon-mean-in-ruby)
- [What is Ruby's double-colon (::) all about?](http://stackoverflow.com/questions/3009477/what-is-rubys-double-colon-all-about)
- [. vs :: (dot vs. double-colon) for calling a method](http://stackoverflow.com/questions/11043450/vs-dot-vs-double-colon-for-calling-a-method)
- [Ways to load code](https://practicingruby.com/articles/ways-to-load-code)
- [Tapping method chains with Ruby 1.9](http://www.infoq.com/news/2008/02/tap-method-ruby19)
- [Eavesdropping on Expressions (more tap)](http://moonbase.rydia.net/mental/blog/programming/eavesdropping-on-expressions)
- [Chaining methods using tap](http://siddharth-ravichandran.com/2010/11/30/chaining-methods-using-tap-method/)
- [Passing functions in Ruby: harder than it looks](http://blog.jessitron.com/2013/03/passing-functions-in-ruby-harder-than.html)
- [Ruby Functional Programming](https://code.google.com/p/tokland/wiki/RubyFunctionalProgramming)
- [Minitest Quick Reference](http://mattsears.com/articles/2011/12/10/minitest-quick-reference)
